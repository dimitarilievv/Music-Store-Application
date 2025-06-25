using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Domain.Domain_Models
{
    public class Album : BaseEntity
    {
        [Required]
        public string? Title { get; set; }

        public string? Description { get; set; }

        [Required]
        public string? CoverImageUrl { get; set; }

        [Required]
        public double Price { get; set; }

        public double Rating { get; set; }
        public Guid GenreId { get; set; }
        public virtual Genre? Genre { get; set; }
        public Guid ArtistId { get; set; }
        public virtual Artist? Artist { get; set; }
        public virtual ICollection<AlbumInShoppingCart>? AllShoppingCarts { get; set; }
        public string? ApiId { get; set; }
    }
}
