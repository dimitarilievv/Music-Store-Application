using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicStore.Domain.Domain_Models;
using MusicStore.Service.Interface;
using MusicStore.Service.Interface.MusicStore.Service.Interface;
using System;
using System.Threading.Tasks;

namespace Music_Store_Application.Controllers
{
    public class ArtistsController : Controller
    {
        private readonly IArtistService _artistService;
        private readonly ILabelService _labelService;

        public ArtistsController(IArtistService artistService, ILabelService labelService)
        {
            _artistService = artistService;
            _labelService = labelService;
        }

        // GET: Artists
        public IActionResult Index()
        {
            var artists = _artistService.GetAll();
            return View(artists);
        }

        // GET: Artists/Details/5
        public IActionResult Details(Guid id)
        {
            var artist = _artistService.GetById(id);
            if (artist == null)
            {
                return NotFound();
            }
            return View(artist);
        }

        // GET: Artists/Create
        public IActionResult Create()
        {
            ViewData["Labels"] = new SelectList(_labelService.GetAll(), "Id", "Name");
            return View();
        }

        // POST: Artists/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FullName,Bio,ImageUrl,BirthDate,Nationality,WebsiteUrl,LabelId")] Artist artist)
        {
            if (ModelState.IsValid)
            {
                artist.Id = Guid.NewGuid();
                _artistService.Insert(artist);
                return RedirectToAction(nameof(Index));
            }
            ViewData["Labels"] = new SelectList(_labelService.GetAll(), "Id", "Name", artist.LabelId);
            return View(artist);
        }

        // GET: Artists/Edit/5
        public IActionResult Edit(Guid id)
        {
            var artist = _artistService.GetById(id);
            if (artist == null)
            {
                return NotFound();
            }
            ViewData["Labels"] = new SelectList(_labelService.GetAll(), "Id", "Name", artist.LabelId);
            return View(artist);
        }

        // POST: Artists/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,FullName,Bio,ImageUrl,BirthDate,Nationality,WebsiteUrl,LabelId")] Artist artist)
        {
            if (id != artist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _artistService.Update(artist);
                }
                catch (Exception)
                {
                    if (!_artistService.Exists(artist.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Labels"] = new SelectList(_labelService.GetAll(), "Id", "Name", artist.LabelId);
            return View(artist);
        }

        // GET: Artists/Delete/5
        public IActionResult Delete(Guid id)
        {
            var artist = _artistService.GetById(id);
            if (artist == null)
            {
                return NotFound();
            }
            return View(artist);
        }

        // POST: Artists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _artistService.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
