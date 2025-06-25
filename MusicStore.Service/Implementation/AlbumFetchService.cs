using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using MusicStore.Domain.Domain_Models;
using MusicStore.Service.Interface;
using System.Net.Http;
using MusicStore.Domain.DTO;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using Newtonsoft.Json;


namespace MusicStore.Service.Implementation
{
    public class AlbumFetchService : IAlbumFetchService
    {
        private readonly HttpClient _httpClient;
        private readonly IAlbumService _albumService;
        private readonly IGenreService _genreService;
        private readonly IArtistService _artistService;

        public AlbumFetchService(HttpClient httpClient, IAlbumService albumService, IGenreService genreService, IArtistService artistService)
        {
            _httpClient = httpClient;
            _albumService = albumService;
            _genreService = genreService;
            _artistService = artistService;

        }
        public async Task ImportAlbumsAsync()
        {

            var url = "https://api.discogs.com/database/search?search=vinyl&type=release&sort=random&per_page=40&key=GpCPyiIPTzGQHKYzXInS&secret=liEyxjeIAqPKUiqUkoRNqlsVXBSWzaEV";

            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("MusicStoreApp/1.0 (dimitar.iliev123@gmail.com)");

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var json = JsonConvert.DeserializeObject<AlbumResponseDTO>(content);
            foreach (var result in json.results)
            {
                string titleFull = result.title;
                string title = titleFull.Contains(" - ") ? titleFull.Split(" - ")[1] : titleFull;
                string artistName = titleFull.Contains(" - ") ? titleFull.Split(" - ")[0] : "Unknown";

                string genre = (result.genre != null && result.genre.Count > 0) ? result.genre[0] : "Unknown";
                string description = result.style != null ? string.Join(", ", result.style) : "";

                string coverImage = result.cover_image;

                int releaseId = result.id;
                double price = await GetPriceAsync(releaseId);

                double rating = await GetRatingAsync(releaseId);

                var artist = _artistService.GetOrCreateByName(artistName);
                var genreEntity = _genreService.GetOrCreateByName(genre);

                string apiId = result.id.ToString();


                var album = new Album
                {
                    Title = title,
                    Description = description,
                    CoverImageUrl = coverImage,
                    Price = price,
                    Rating = rating,
                    Artist = artist,
                    Genre = genreEntity,
                    ArtistId = artist.Id,
                    GenreId = genreEntity.Id,
                    ApiId = apiId
                };

                _albumService.Insert(album);
                await Task.Delay(1000);
            }
        }
        private async Task<double> GetPriceAsync(int releaseId)
        {
            var statsUrl = $"https://api.discogs.com/marketplace/stats/{releaseId}";
            var response = await _httpClient.GetAsync(statsUrl);

            if (!response.IsSuccessStatusCode)
                return 20.0;

            var content = await response.Content.ReadAsStringAsync();
            var stats = JsonConvert.DeserializeObject<AlbumPriceDTO>(content);

            return stats?.lowest_price?.Value ?? 20.0;
        }
        private async Task<double> GetRatingAsync(int releaseId)
        {
            var releaseUrl = $"https://api.discogs.com/releases/{releaseId}";
            var response = await _httpClient.GetAsync(releaseUrl);

            if (!response.IsSuccessStatusCode)
                return 0.0;

            var content = await response.Content.ReadAsStringAsync();
            var ratingData = JsonConvert.DeserializeObject<RatingDTO>(content);

            return ratingData?.community?.rating?.average ?? 0.0;
        }
    }
}

