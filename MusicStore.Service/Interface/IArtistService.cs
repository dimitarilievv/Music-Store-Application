using MusicStore.Domain.Domain_Models;
using System;
using System.Collections.Generic;

namespace MusicStore.Service.Interface
{
    public interface IArtistService
    {
        List<Artist> GetAll();
        Artist? GetById(Guid id);
        Artist Insert(Artist artist);
        Artist Update(Artist artist);
        Artist DeleteById(Guid id);
        bool Exists(Guid id);
        Artist GetOrCreateByName(string name);
    }
}
