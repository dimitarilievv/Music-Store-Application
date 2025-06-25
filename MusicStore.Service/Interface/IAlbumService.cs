using MusicStore.Domain.Domain_Models;
using MusicStore.Domain.DTO;
using System;
using System.Collections.Generic;

namespace MusicStore.Service.Interface
{
    public interface IAlbumService
    {
        List<Album> GetAll();
        Album? GetById(Guid id);
        Album Insert(Album album);
        Album Update(Album album);
        Album DeleteById(Guid id);
        AddToCartDTO GetSelectedShoppingCartAlbum(Guid id);
        void AddAlbumToShoppingCart(Guid albumId, Guid userId, int quantity);
        bool ExistsByTitle(string title);
    }
}
