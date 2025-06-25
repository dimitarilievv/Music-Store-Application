using MusicStore.Domain.Domain_Models;
using System.Collections.Generic;

namespace MusicStore.Domain.DTO
{
    public class ShoppingCartDTO
    {
        public List<AlbumInShoppingCart> Albums { get; set; }
        public double TotalPrice { get; set; }
    }
}
