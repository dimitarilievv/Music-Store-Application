using MusicStore.Domain.Domain_Models;
using MusicStore.Repository.Interface;
using MusicStore.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicStore.Service.Implementation
{
    public class GenreService : IGenreService
    {
        private readonly IRepository<Genre> _genreRepository;

        public GenreService(IRepository<Genre> genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public List<Genre> GetAll()
        {
            return _genreRepository.GetAll(selector: x => x).ToList();
        }

        public Genre? GetById(Guid id)
        {
            return _genreRepository.Get(selector: x => x,
                                        predicate: x => x.Id == id);
        }

        public Genre Insert(Genre genre)
        {
            genre.Id = Guid.NewGuid();
            return _genreRepository.Insert(genre);
        }

        public Genre Update(Genre genre)
        {
            return _genreRepository.Update(genre);
        }

        public Genre DeleteById(Guid id)
        {
            var genre = GetById(id);
            if (genre == null)
                throw new Exception("Genre not found");

            _genreRepository.Delete(genre);
            return genre;
        }

        public bool Exists(Guid id)
        {
            var genre = _genreRepository.Get(x => x, x => x.Id == id);
            return genre != null;
        }

        public Genre GetOrCreateByName(string name)
        {
            var genre = _genreRepository.GetAll(selector: x => x,
                                           predicate: x => x.Name == name)
                                           .FirstOrDefault();

            if (genre == null)
            {
                var temp = new Genre
                {
                    Name = name
                };
                _genreRepository.Insert(temp);
                return temp;
            }
            return genre;
        }
    }
}
