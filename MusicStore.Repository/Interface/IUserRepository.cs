using MusicStore.Domain.Identity_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Repository.Interface
{
    public interface IUserRepository
    {
        MusicStoreApplicationUser GetUserById(string id);
    }
}
