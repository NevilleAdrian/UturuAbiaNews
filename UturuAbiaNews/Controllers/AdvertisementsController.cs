using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UturuAbiaNews.Data;
using UturuAbiaNews.Models;

namespace UturuAbiaNews.Controllers
{
	public class AdvertViewModel : Advertisement
	{
		public IFormFile AdUrl { get; set; }
	}
    public class AdvertisementsController : Controller
    {
		private IHostingEnvironment _env;
		private readonly ApplicationDbContext _context;

        public AdvertisementsController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;
			_env = environment;
        }

        // GET: Advertisements
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Advertisement.Include(a => a.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Advertisements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisement = await _context.Advertisement
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (advertisement == null)
            {
                return NotFound();
            }

            return View(advertisement);
        }

        // GET: Advertisements/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Category, "ID", "CategoryName");
            return View();
        }

        // POST: Advertisements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AdvertName,AdvertDescription,AdUrl,StartDate,EndDate,Position,CategoryID")] AdvertViewModel advertViewModel)
        {
			Advertisement advertisement = new Advertisement();

			if (ModelState.IsValid)
            {
				IFormFile file = advertViewModel.AdUrl;
				string relativePath = "";
				if (file != null)
				{
					var fileName = $"{advertViewModel.ID}{Path.GetFileName(file.FileName)}";
					relativePath = Path.Combine("images", "ads", fileName);
					var absolutePath = Path.Combine(_env.WebRootPath, relativePath);

					using (FileStream stream = new FileStream(absolutePath, FileMode.Create))
					{
						file.CopyTo(stream);
					}

				}
				advertisement.AdvertDescription = advertViewModel.AdvertDescription;
				advertisement.AdvertImage = relativePath;
				advertisement.AdvertName = advertViewModel.AdvertName;
				advertisement.Category = advertViewModel.Category;
				advertisement.CategoryID = advertViewModel.CategoryID;
				advertisement.EndDate = advertViewModel.EndDate;
				advertisement.ID = advertViewModel.ID;
				advertisement.StartDate = advertViewModel.StartDate;
				advertisement.Position = advertViewModel.Position;



				_context.Add(advertisement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_context.Category, "ID", "CategoryName", advertisement.CategoryID);
            return View(advertisement);
        }

        // GET: Advertisements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisement = await _context.Advertisement.FindAsync(id);
            if (advertisement == null)
            {
                return NotFound();
            }
			AdvertViewModel advertViewModel = new AdvertViewModel
			{
				AdvertDescription = advertisement.AdvertDescription,
				AdvertImage = advertisement.AdvertImage,
				AdvertName = advertisement.AdvertName,
				Category = advertisement.Category,
				CategoryID = advertisement.CategoryID,
				EndDate = advertisement.EndDate,
				StartDate = advertisement.StartDate,
				ID = advertisement.ID,
				Position = advertisement.Position
			};
            ViewData["CategoryID"] = new SelectList(_context.Category, "ID", "CategoryName", advertisement.CategoryID);
            return View(advertViewModel);
        }

        // POST: Advertisements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,AdvertName,AdvertDescription,AdvertImage,StartDate,Position,EndDate,CategoryID,AdUrl")] AdvertViewModel advertViewModel)
        {
			Advertisement advertisement = new Advertisement();
            if (id != advertViewModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
				IFormFile file = advertViewModel.AdUrl;
				string relativePath = "";
				if (file != null)
				{
					if (advertViewModel.AdvertImage != null)
					{
						var oldPath = Path.Combine(_env.WebRootPath, advertViewModel.AdvertImage);
						if (System.IO.File.Exists(oldPath))
						{
							System.IO.File.Delete(oldPath);
						}
					}

					var fileName = $"{advertViewModel.ID}{Path.GetFileName(file.FileName)}";
					relativePath = Path.Combine("images", "ads", fileName);
					var absolutePath = Path.Combine(_env.WebRootPath, relativePath);

					using (FileStream stream = new FileStream(absolutePath, FileMode.Create))
					{
						file.CopyTo(stream);
					}

				}
				try
                {
					advertisement.AdvertDescription = advertViewModel.AdvertDescription;
					advertisement.AdvertImage = (!string.IsNullOrEmpty(relativePath)) ? relativePath : advertViewModel.AdvertImage;
					advertisement.AdvertName = advertViewModel.AdvertName;
					advertisement.Category = advertViewModel.Category;
					advertisement.CategoryID = advertViewModel.CategoryID;
					advertisement.EndDate = advertViewModel.EndDate;
					advertisement.ID = advertViewModel.ID;
					advertisement.StartDate = advertViewModel.StartDate;
					advertisement.Position = advertViewModel.Position;

					_context.Update(advertisement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdvertisementExists(advertisement.ID))
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
            ViewData["CategoryID"] = new SelectList(_context.Category, "ID", "CategoryName", advertisement.CategoryID);
            return View(advertisement);
        }

        // GET: Advertisements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisement = await _context.Advertisement
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (advertisement == null)
            {
                return NotFound();
            }

            return View(advertisement);
        }

        // POST: Advertisements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var advertisement = await _context.Advertisement.FindAsync(id);
			if (string.IsNullOrEmpty(advertisement.AdvertImage))
			{
				advertisement.AdvertImage = "";
			}
			var oldPath = Path.Combine(_env.WebRootPath, advertisement.AdvertImage);
			if (System.IO.File.Exists(oldPath))
			{
				System.IO.File.Delete(oldPath);
			}
			_context.Advertisement.Remove(advertisement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdvertisementExists(int id)
        {
            return _context.Advertisement.Any(e => e.ID == id);
        }
    }
}
