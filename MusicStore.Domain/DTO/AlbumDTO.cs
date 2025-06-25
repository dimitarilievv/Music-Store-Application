using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Domain.DTO
{
    public class AlbumDTO
    {
        public int id { get; set; }
        public string title { get; set; }
        public List<string> genre { get; set; }
        public List<string> style { get; set; }
        public string cover_image { get; set; }
    }
}
