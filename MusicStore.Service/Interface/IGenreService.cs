using MusicStore.Domain.Domain_Models;
using System;
using System.Collections.Generic;

namespace MusicStore.Service.Interface
{
    public interface IGenreService
    {
        List<Genre> GetAll();
        Genre? GetById(Guid id);
        Genre Insert(Genre genre);
        Genre Update(Genre genre);
        Genre DeleteById(Guid id);
        bool Exists(Guid id);
        Genre GetOrCreateByName(string name);
    }
}
