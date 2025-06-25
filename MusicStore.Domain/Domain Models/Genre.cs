using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicStore.Domain.Domain_Models
{
    public class Genre : BaseEntity
    {
        [Required]
        public string? Name { get; set; }
        public virtual ICollection<Album>? Albums { get; set; }
    }
}
