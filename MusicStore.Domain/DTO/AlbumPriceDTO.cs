using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MusicStore.Domain.DTO
{
    public class AlbumPriceDTO
    {
        [JsonProperty("lowest_price")]
        public PriceDetailsDTO lowest_price { get; set; }
    }
}
