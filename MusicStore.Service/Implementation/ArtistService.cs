using Microsoft.EntityFrameworkCore;
using MusicStore.Domain.Domain_Models;
using MusicStore.Repository.Interface;
using MusicStore.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Net.WebRequestMethods;

namespace MusicStore.Service.Implementation
{
    public class ArtistService : IArtistService
    {
        private readonly IRepository<Artist> _artistRepository;

        public ArtistService(IRepository<Artist> artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public List<Artist> GetAll()
        {
            return _artistRepository.GetAll(selector: x => x).ToList();
        }

        public Artist? GetById(Guid id)
        {
            return _artistRepository.Get(selector: x => x,
                                         predicate: x => x.Id == id,
                                         include: x=>x.Include(y=>y.Label));
        }

        public Artist Insert(Artist artist)
        {
            artist.Id = Guid.NewGuid();
            return _artistRepository.Insert(artist);
        }

        public Artist Update(Artist artist)
        {
            return _artistRepository.Update(artist);
        }

        public Artist DeleteById(Guid id)
        {
            var artist = GetById(id);
            if (artist == null)
                throw new Exception("Artist not found");

            _artistRepository.Delete(artist);
            return artist;
        }

        public bool Exists(Guid id)
        {
            var artist = _artistRepository.Get(selector: x => x, predicate: x => x.Id == id);
            return artist != null;
        }

        public Artist GetOrCreateByName(string name)
        {
            var artist = _artistRepository.GetAll(selector: x => x,
                                            predicate: x => x.FullName == name)
                                            .FirstOrDefault();

            if (artist == null)
            {
                var temp = new Artist
                {
                    FullName = name,
                    ImageUrl= "https://t4.ftcdn.net/jpg/04/70/29/97/360_F_470299797_UD0eoVMMSUbHCcNJCdv2t8B2g1GVqYgs.jpg"
                }
            ;
                _artistRepository.Insert(temp);
                return temp;
            }
            return artist;
        }
    }
}
