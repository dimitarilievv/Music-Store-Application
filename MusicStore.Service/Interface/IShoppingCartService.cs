using System;
using System.Collections.Generic;
using MusicStore.Domain.Domain_Models;
using MusicStore.Domain.DTO;

namespace MusicStore.Service.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCart? GetByUserId(Guid userId);
        ShoppingCartDTO GetByUserIdWithIncludedAlbums(Guid userId); 
        void DeleteAlbumFromShoppingCart(Guid albumInShoppingCartId);
        bool OrderAlbums(Guid userId); 
        //void Create(ShoppingCart cart); 
    }
}
