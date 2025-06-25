using Microsoft.EntityFrameworkCore;
using MusicStore.Domain.Domain_Models;
using MusicStore.Domain.DTO;
using MusicStore.Repository.Interface;
using MusicStore.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicStore.Service.Implementation
{
    public class AlbumService : IAlbumService
    {
        private readonly IRepository<Album> _albumRepository;
        private readonly IRepository<AlbumInShoppingCart> _albumInShoppingCartRepository;
        private readonly IShoppingCartService _shoppingCartService;

        public AlbumService(
            IRepository<Album> albumRepository,
            IRepository<AlbumInShoppingCart> albumInShoppingCartRepository,
            IShoppingCartService shoppingCartService)
        {
            _albumRepository = albumRepository;
            _albumInShoppingCartRepository = albumInShoppingCartRepository;
            _shoppingCartService = shoppingCartService;
        }

        public void AddAlbumToShoppingCart(Guid albumId, Guid userId, int quantity)
        {
            var shoppingCart = _shoppingCartService.GetByUserId(userId);

            if (shoppingCart == null)
            {
                throw new Exception("Shopping cart not found");
            }

            var album = GetById(albumId);

            if (album == null)
            {
                throw new Exception("Album not found");
            }

            UpdateCartItem(album, shoppingCart, quantity);
        }

        private AlbumInShoppingCart? GetAlbumInShoppingCart(Guid albumId, Guid cartId)
        {
            return _albumInShoppingCartRepository.Get(
                selector: x => x,
                predicate: x => x.ShoppingCartId == cartId && x.AlbumId == albumId
            );
        }

        private void UpdateCartItem(Album album, ShoppingCart shoppingCart, int quantity)
        {
            var existingAlbum = GetAlbumInShoppingCart(album.Id, shoppingCart.Id);

            if (existingAlbum == null)
            {
                var albumInShoppingCart = new AlbumInShoppingCart
                {
                    Id = Guid.NewGuid(),
                    AlbumId = album.Id,
                    ShoppingCartId = shoppingCart.Id,
                    Album = album,
                    ShoppingCart = shoppingCart,
                    Quantity = quantity
                };

                _albumInShoppingCartRepository.Insert(albumInShoppingCart);
            }
            else
            {
                existingAlbum.Quantity += quantity;
                _albumInShoppingCartRepository.Update(existingAlbum);
            }
        }

        public Album DeleteById(Guid id)
        {
            var album = GetById(id);
            if (album == null)
            {
                throw new Exception("Album not found");
            }

            _albumRepository.Delete(album);
            return album;
        }

        public List<Album> GetAll()
        {
            return _albumRepository.GetAll(selector: x => x,
                include:x=>x.Include(x=>x.Artist))
                .ToList();
        }

        public Album? GetById(Guid id)
        {
            return _albumRepository.Get(
                selector: x => x,
                predicate: x => x.Id == id              
            );
        }

        public Album Insert(Album album)
        {
            var existingAlbum = _albumRepository
                    .GetAll(a => a, a => a.ApiId == album.ApiId)
                    .FirstOrDefault();

            if (existingAlbum == null)
            {
                album.Id = Guid.NewGuid();
                _albumRepository.Insert(album);
                return album;
            }
            else
            {
                return existingAlbum;
            }
        }

        public Album Update(Album album)
        {
            return _albumRepository.Update(album);
        }

        public AddToCartDTO GetSelectedShoppingCartAlbum(Guid id)
        {
            var selectedAlbum = GetById(id);

            if (selectedAlbum == null)
            {
                throw new Exception("Album not found");
            }

            return new AddToCartDTO
            {
                SelectedAlbumId = selectedAlbum.Id,
                SelectedAlbumTitle = selectedAlbum.Title,
                Quantity = 1
            };
        }
        public bool ExistsByTitle(string title)
        {
            return _albumRepository.Get(a => a.Title == title) != null;
        }
    }
}
