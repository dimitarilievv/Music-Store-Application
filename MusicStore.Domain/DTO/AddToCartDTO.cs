using System;

namespace MusicStore.Domain.DTO
{
    public class AddToCartDTO
    {
        public Guid SelectedAlbumId { get; set; }
        public string? SelectedAlbumTitle { get; set; }
        public int Quantity { get; set; }
    }
}