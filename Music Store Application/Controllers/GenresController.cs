using Microsoft.AspNetCore.Mvc;
using MusicStore.Domain.Domain_Models;
using MusicStore.Service.Interface;

namespace Music_Store_Application.Controllers
{
    public class GenresController : Controller
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        public IActionResult Index()
        {
            var genres = _genreService.GetAll();
            return View(genres);
        }

        public IActionResult Details(Guid? id)
        {
            if (id == null)
                return NotFound();

            var genre = _genreService.GetById(id.Value);
            if (genre == null)
                return NotFound();

            return View(genre);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Id")] Genre genre)
        {
            if (ModelState.IsValid)
            {
                _genreService.Insert(genre);
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }

        public IActionResult Edit(Guid? id)
        {
            if (id == null)
                return NotFound();

            var genre = _genreService.GetById(id.Value);
            if (genre == null)
                return NotFound();

            return View(genre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Name,Id")] Genre genre)
        {
            if (id != genre.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _genreService.Update(genre);
                }
                catch (Exception)
                {
                    if (!_genreService.Exists(genre.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }

        public IActionResult Delete(Guid? id)
        {
            if (id == null)
                return NotFound();

            var genre = _genreService.GetById(id.Value);
            if (genre == null)
                return NotFound();

            return View(genre);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _genreService.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
