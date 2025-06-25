using Microsoft.AspNetCore.Mvc;
using MusicStore.Domain.Domain_Models;
using MusicStore.Service.Interface.MusicStore.Service.Interface;

namespace Music_Store_Application.Controllers
{
    public class LabelsController : Controller
    {
        private readonly ILabelService _labelService;

        public LabelsController(ILabelService labelService)
        {
            _labelService = labelService;
        }

        // GET: Labels
        public IActionResult Index()
        {
            var labels = _labelService.GetAll();
            return View(labels);
        }

        // GET: Labels/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
                return NotFound();

            var label = _labelService.GetById(id.Value);
            if (label == null)
                return NotFound();

            return View(label);
        }

        // GET: Labels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Labels/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Country,FoundedYear,Id")] Label label)
        {
            if (ModelState.IsValid)
            {
                _labelService.Insert(label);
                return RedirectToAction(nameof(Index));
            }
            return View(label);
        }

        // GET: Labels/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
                return NotFound();

            var label = _labelService.GetById(id.Value);
            if (label == null)
                return NotFound();

            return View(label);
        }

        // POST: Labels/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Name,Country,FoundedYear,Id")] Label label)
        {
            if (id != label.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _labelService.Update(label);
                }
                catch (Exception)
                {
                    if (!_labelService.Exists(label.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(label);
        }

        // GET: Labels/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
                return NotFound();

            var label = _labelService.GetById(id.Value);
            if (label == null)
                return NotFound();

            return View(label);
        }

        // POST: Labels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _labelService.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
