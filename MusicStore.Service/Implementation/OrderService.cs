using Microsoft.EntityFrameworkCore;
using MusicStore.Domain.Domain_Models;
using MusicStore.Repository.Interface;
using MusicStore.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;

        public OrderService(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public List<Order> GetAllOrders()
        {
            return _orderRepository.GetAll(selector: x => x,
                                           include: x => x.Include(z => z.AlbumInOrders)
                                                          .ThenInclude(z => z.OrderedAlbum)
                                                          .Include(z=>z.Owner)).ToList();
        }

        public Order GetOrder(Guid Id)
        {
            return _orderRepository.Get(selector: x => x,
                                        predicate: x => x.Id.Equals(Id),
                                        include: x => x.Include(z => z.AlbumInOrders)
                                                       .ThenInclude(z => z.OrderedAlbum)
                                                       .Include(z => z.Owner));
        }
    }
}
