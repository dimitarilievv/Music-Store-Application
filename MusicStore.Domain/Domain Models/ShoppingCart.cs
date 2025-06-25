using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicStore.Domain.Identity_Models;

namespace MusicStore.Domain.Domain_Models
{
    public class ShoppingCart : BaseEntity
    {
        public string? OwnerId { get; set; }
        public MusicStoreApplicationUser? Owner { get; set; }
        public virtual ICollection<AlbumInShoppingCart>? AllAlbums { get; set; }

    }
}
