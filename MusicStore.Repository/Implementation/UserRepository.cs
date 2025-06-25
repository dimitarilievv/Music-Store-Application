using Microsoft.EntityFrameworkCore;
using MusicStore.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicStore.Domain.Identity_Models;

namespace MusicStore.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly DbSet<MusicStoreApplicationUser> entites;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
            this.entites = _context.Set<MusicStoreApplicationUser>();
        }

        public MusicStoreApplicationUser GetUserById(string id)
        {
            return entites.First(ent => ent.Id == id);
        }
    }
}
