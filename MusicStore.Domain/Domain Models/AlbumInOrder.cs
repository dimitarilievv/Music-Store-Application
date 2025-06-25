using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Domain.Domain_Models
{
    public class AlbumInOrder : BaseEntity
    {
        public Guid AlbumId { get; set; }
        public Album? OrderedAlbum { get; set; }

        public Guid OrderId { get; set; }
        public Order? Order { get; set; }

        public int Quantity { get; set; }
    }
}
