using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicStore.Domain.Domain_Models;
using MusicStore.Domain.DTO;
using MusicStore.Service.Interface;

namespace EShop.Web.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly IAlbumService _albumService;
        private readonly IArtistService _artistService;
        private readonly IGenreService _genreService;
        private readonly IAlbumFetchService _albumFetchService;

        public AlbumsController(IAlbumService albumService, IArtistService artistService, IGenreService genreService, IAlbumFetchService albumFetchService)
        {
            _albumService = albumService;
            _artistService = artistService;
            _genreService = genreService;
            _albumFetchService = albumFetchService;
        }

        // GET: Albums
        public async Task<IActionResult> Index()
        {
            var albums = _albumService.GetAll();
            return View(albums);
        }
        [HttpPost]
        public async Task<IActionResult> ImportAlbums()
        {
            await _albumFetchService.ImportAlbumsAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Albums/Details/5
        public IActionResult Details(Guid id)
        {
            var album = _albumService.GetById(id);
            if (album == null)
                return NotFound();

            return View(album);
        }

        // GET: Albums/Create
        public IActionResult Create()
        {
            ViewData["Artists"] = new SelectList(_artistService.GetAll(), "Id", "FullName"); ;
            ViewData["Genres"] = new SelectList(_genreService.GetAll(), "Id", "Name");
            return View();
        }

        // POST: Albums/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Title,Description,CoverImageUrl,Price,Rating,GenreId,ArtistId,ApiId")] Album album)
        {
            if (ModelState.IsValid)
            {
                _albumService.Insert(album);
                return RedirectToAction(nameof(Index));
            }

            ViewData["Artists"] = new SelectList(_artistService.GetAll(), "Id", "FullName");
            ViewData["Genres"] = new SelectList(_genreService.GetAll(), "Id", "Name");

            return View(album);
        }

        // GET: Albums/Edit/5
        public IActionResult Edit(Guid id)
        {
            var album = _albumService.GetById(id);
            if (album == null)
                return NotFound();
            ViewData["Artists"] = new SelectList(_artistService.GetAll(), "Id", "FullName");
            ViewData["Genres"] = new SelectList(_genreService.GetAll(), "Id", "Name");
            return View(album);
        }

        // POST: Albums/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,Title,Description,CoverImageUrl,Price,Rating,GenreId,ArtistId,ApiId")] Album album)
        {
            if (id != album.Id)
                return NotFound();
            if (ModelState.IsValid)
            {
                _albumService.Update(album);
                return RedirectToAction(nameof(Index));
            }

            ViewData["Artists"] = new SelectList(_artistService.GetAll(), "Id", "FullName");
            ViewData["Genres"] = new SelectList(_genreService.GetAll(), "Id", "Name");

            return View(album);
        }

        // GET: Albums/Delete/5
        public IActionResult Delete(Guid id)
        {
            var album = _albumService.GetById(id);
            if (album == null)
                return NotFound();
            ViewData["artist"] = _artistService.GetById(album.ArtistId);
            ViewData["genre"] = _genreService.GetById(album.GenreId);
            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _albumService.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }

        // Add album to shopping cart - GET
        [Authorize]
        public IActionResult AddAlbumToCart(Guid id)
        {
            AddToCartDTO model = _albumService.GetSelectedShoppingCartAlbum(id);
            return View(model);
        }

        // Add album to shopping cart - POST
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddAlbumToCart(AddToCartDTO model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized();

            _albumService.AddAlbumToShoppingCart(model.SelectedAlbumId, Guid.Parse(userId), model.Quantity);
            return RedirectToAction(nameof(Index));
        }
    }
}
