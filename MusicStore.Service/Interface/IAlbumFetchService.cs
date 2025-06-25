using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicStore.Domain.Domain_Models;
using MusicStore.Domain.DTO;

namespace MusicStore.Service.Interface
{
    public interface IAlbumFetchService
    {
        Task ImportAlbumsAsync();
    }
}
