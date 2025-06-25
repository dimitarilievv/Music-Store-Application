using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicStore.Domain.Domain_Models
{
    public class Artist : BaseEntity
    {
        [Required]
        public string FullName { get; set; }
        public string? Bio { get; set; }
        public string? ImageUrl { get; set; }
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
        public string? Nationality { get; set; }
        public string? WebsiteUrl { get; set; }
       
        public virtual ICollection<Album>? Albums { get; set; }
        public Guid? LabelId { get; set; }
        public virtual Label? Label { get; set; }
    }
}
