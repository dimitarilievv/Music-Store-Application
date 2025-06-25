using Microsoft.EntityFrameworkCore;
using MusicStore.Domain.Domain_Models;
using MusicStore.Domain.DTO;
using MusicStore.Domain.Email;
using MusicStore.Repository.Interface;
using MusicStore.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicStore.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<AlbumInShoppingCart> _albumInShoppingCartRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<AlbumInOrder> _albumInOrderRepository;
        private readonly IEmailService _emailService;


        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository,
                                    IRepository<AlbumInShoppingCart> albumInShoppingCartRepository,
                                    IUserRepository userRepository,
                                    IRepository<Order> orderRepository,
                                    IRepository<AlbumInOrder> albumInOrderRepository,
                                    IEmailService emailService)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _albumInShoppingCartRepository = albumInShoppingCartRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _albumInOrderRepository = albumInOrderRepository;
            _emailService = emailService;
        }

        public ShoppingCart? GetByUserId(Guid userId)
        {
            return _shoppingCartRepository.Get(selector:x => x,
                                                predicate:x => x.OwnerId.Equals(userId.ToString()));
        }

        public void DeleteAlbumFromShoppingCart(Guid albumInShoppingCartId)
        {
            var albumInShoppingCart = _albumInShoppingCartRepository
                .Get(selector: x => x,
                predicate: x => x.Id.Equals(albumInShoppingCartId));
            if (albumInShoppingCart == null)
            {
                throw new Exception("Album in shopping cart not found");
            }

            _albumInShoppingCartRepository.Delete(albumInShoppingCart);
        }

        public ShoppingCartDTO GetByUserIdWithIncludedAlbums(Guid userId)
        {
            var userCart = _shoppingCartRepository.Get(
                selector: x => x,
                predicate: x => x.OwnerId.Equals(userId.ToString()),
                include: x => x.Include(z => z.AllAlbums).ThenInclude(m => m.Album));

            var allAlbums = userCart.AllAlbums.ToList();

            var allAlbumsPrices = allAlbums.Select(z => new
            {
                AlbumPrice = z.Album.Price,
                Quantity = z.Quantity
            }).ToList();

            var totalPrice = 0.0;

            foreach (var item in allAlbumsPrices)
            {
                totalPrice += item.Quantity * item.AlbumPrice;
            }

            return new ShoppingCartDTO
            {
                Albums = allAlbums,
                TotalPrice = totalPrice
            };
        }

        public bool OrderAlbums(Guid userId)
        {
            var userCart = _shoppingCartRepository.Get(
                selector: x => x,
                predicate: x => x.OwnerId.Equals(userId.ToString()),
                include: x => x.Include(z => z.AllAlbums).ThenInclude(m => m.Album));

            var loggedInUser = _userRepository.GetUserById(userId.ToString());

            var emailMessage = new EmailMessage();
            emailMessage.MailTo = loggedInUser.Email;
            emailMessage.Subject = "Successful Album Order";

            var order = new Order
            {
                Id = Guid.NewGuid(),
                OwnerId = loggedInUser.Id,
                Owner = loggedInUser
            };

            _orderRepository.Insert(order);

            var albumsInOrder = userCart.AllAlbums.Select(x => new AlbumInOrder
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                Order = order,
                AlbumId = x.AlbumId,
                OrderedAlbum = x.Album,
                Quantity = x.Quantity
            }).ToList();

            StringBuilder sb = new StringBuilder();
            double totalPrice = 0.0;

            sb.AppendLine("🎵 Your album order has been placed successfully!");
            sb.AppendLine();
            sb.AppendLine("🛒 Order Summary:");
            sb.AppendLine("--------------------------------------------");

            int counter = 1;
            foreach (var item in albumsInOrder)
            {
                totalPrice += item.Quantity * item.OrderedAlbum.Price;
                sb.AppendLine($"{counter}. {item.OrderedAlbum.Title}");
                sb.AppendLine($"   Quantity: {item.Quantity}");
                sb.AppendLine($"   Price: ${item.OrderedAlbum.Price:F2}");
                sb.AppendLine();
                counter++;
            }

            sb.AppendLine("--------------------------------------------");
            sb.AppendLine($"🧾 Total Price: ${totalPrice:F2}");
            sb.AppendLine();
            sb.AppendLine("🎧 Thank you for shopping with Music Store!");

            emailMessage.Content = sb.ToString();

            foreach (var album in albumsInOrder)
            {
                _albumInOrderRepository.Insert(album);
            }

            userCart.AllAlbums.Clear();

            _shoppingCartRepository.Update(userCart);
            _emailService.SendEmailAsync(emailMessage);


            return true;
        }

    }
}