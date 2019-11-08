using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UturuAbiaNews.Data;
using UturuAbiaNews.Models;

namespace UturuAbiaNews.Controllers
{
	public class CategoryPageViewModel
	{
		public ICollection<Content> Contents { get; set; }
		public ICollection<Advertisement> TopRightAdvertisements { get; set; }
		public ICollection<Advertisement> BottomRightAdvertisements { get; set; }
		public ICollection<Advertisement> BottomAdvertisements { get; set; }
		public ICollection<Content> Trending { get; set; }
	}
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {

            return View(await _context.Category.ToListAsync());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category
				.Include(c => c.Contents)
				.Include(a => a.Advertisements)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (category == null)
            {
                return NotFound();
            }

			CategoryPageViewModel catcont = new CategoryPageViewModel
			{
				Contents = new List<Content>(),
				TopRightAdvertisements = new List<Advertisement>(),
				BottomRightAdvertisements = new List<Advertisement>(),
				BottomAdvertisements = new List<Advertisement>(),
				Trending = new List<Content>()
			};

			//get all categories
			foreach (Content content in category.Contents)
			{
				if (content.NoOfViews >= 2)
				{
					catcont.Trending.Add(content);
				}
				
				catcont.Contents.Add(content);
			}

			//get all adverts
			
			foreach (var advert in category.Advertisements)
			{
				var duration = advert.EndDate - DateTime.Now;
				if (DateTime.Now <= advert.EndDate && duration > TimeSpan.Zero)
				{
					if (advert.Position == 1)
					{
						catcont.TopRightAdvertisements.Add(advert);
					}
					else if (advert.Position == 2)
					{
						catcont.BottomRightAdvertisements.Add(advert);
					}
					else
					{
						catcont.BottomAdvertisements.Add(advert);
					}

				}
			}
			


			return View(catcont);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CategoryName,IsFeautured")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CategoryName,IsFeautured")] Category category)
        {
            if (id != category.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.ID))
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
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category
                .FirstOrDefaultAsync(m => m.ID == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Category.FindAsync(id);
            _context.Category.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Category.Any(e => e.ID == id);
        }
    }
}
