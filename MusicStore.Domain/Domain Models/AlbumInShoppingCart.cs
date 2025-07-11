﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Domain.Domain_Models
{
    public class AlbumInShoppingCart : BaseEntity
    {
        public Guid AlbumId { get; set; }
        public Album? Album { get; set; }

        public Guid ShoppingCartId { get; set; }
        public ShoppingCart? ShoppingCart { get; set; }

        public int Quantity { get; set; }
    }
}
