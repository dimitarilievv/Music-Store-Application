using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Domain.Domain_Models
{
    public class Label : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public string? Country { get; set; }

        public int FoundedYear { get; set; }

        public virtual ICollection<Album>? Albums { get; set; }
    }
}
