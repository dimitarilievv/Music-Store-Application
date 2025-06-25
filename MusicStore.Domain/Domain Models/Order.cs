using MusicStore.Domain.Identity_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Domain.Domain_Models
{
    public class Order : BaseEntity
    {
        public string? OwnerId { get; set; }
        public MusicStoreApplicationUser? Owner { get; set; }

        public ICollection<AlbumInOrder>? AlbumInOrders { get; set; }

    }
}
