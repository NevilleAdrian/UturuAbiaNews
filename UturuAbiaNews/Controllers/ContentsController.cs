using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting; 
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UturuAbiaNews.Data;
using UturuAbiaNews.Models;

namespace UturuAbiaNews.Controllers
{
	public class ContentCreateViewModel : Content
	{
		public IFormFile Url { get; set; }
	}
	public class ContentPageViewModel
	{
		public Content Content { get; set; }
		public ICollection<Advertisement> TopRightAdvertisements { get; set; }
		public ICollection<Advertisement> BottomRightAdvertisements { get; set; }
		public ICollection<Advertisement> BottomAdvertisements { get; set; }
		public ICollection<Content> Trending { get; set; }
		public ICollection<Content> RelatedNews { get; set; }
		public ICollection<Comment> Comments { get; set; }
	}
	public class ContentViewModel
	{
		public ICollection<Content> Contents { get; set; }
		public ICollection<Advertisement> Advertisements { get; set; }
		public ICollection<Content> Trending { get; set; }
	}
    public class ContentsController : Controller
    {
		private IHostingEnvironment _env;
        private readonly ApplicationDbContext _context;

        public ContentsController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;
			_env = environment;
        }

        // GET: Contents
		
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Content.Include(c => c.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Contents/Details/5
        public async Task<IActionResult> Details(int? id, int? fromComment)
        {
            if (id == null)
            {
                return NotFound();
            }

			var content = await _context.Content
                .Include(c => c.Category)
				.Include(co => co.Comments)
				.Include(a => a.Category.Advertisements)
                .FirstOrDefaultAsync(m => m.ID == id);

			var categryContext = await _context.Category
				.Include(c => c.Contents)
				.Include(a => a.Advertisements)
				.FirstOrDefaultAsync(m => m.ID == content.CategoryID);
			if (content == null)
            {
                return NotFound();
            }
			content.NoOfViews += 1;
			if (fromComment != null)
			{
				ViewData["Posted"] = "Your message has been added";
				content.NoOfComments += 1;

			}
			else
				ViewData["Posted"] = "";
			ContentPageViewModel catcont = new ContentPageViewModel
			{
				Content = content,
				TopRightAdvertisements = new List<Advertisement>(),
				BottomRightAdvertisements = new List<Advertisement>(),
				BottomAdvertisements = new List<Advertisement>(),
				Trending = new List<Content>(),
				RelatedNews = new List<Content>(),
				Comments = (content.Comments != null) ? content.Comments : new List<Comment>()
			};

			foreach (Content trending in categryContext.Contents)
			{
				if (content.NoOfViews >= 2)
				{
					catcont.Trending.Add(trending);
				}
				catcont.RelatedNews.Add(trending);

			}

			//get all adverts

			foreach (var advert in categryContext.Advertisements)
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
			await _context.SaveChangesAsync();
            return View(catcont);
        }

		// GET: Contents/Create
		
		public IActionResult Create()
        {
			ContentCreateViewModel content = new ContentCreateViewModel();
            ViewData["CategoryID"] = new SelectList(_context.Category, "ID", "CategoryName");
            return View(content);
        }

		// POST: Contents/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
	
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ContentTitle,ShortDescription,Url,LongDescription,NoOfLikes,IsEdited,IsHeadline,DatePublished,DateEdited,VideoUrl,CategoryID")] ContentCreateViewModel contentView)
        {
			Content content = new Content();
			if (ModelState.IsValid)
            {
				
				IFormFile file = contentView.Url;
				string relativePath = "";
				if (file != null)
				{
					var fileName = $"{contentView.ID}{Path.GetFileName(file.FileName)}";
					relativePath = Path.Combine("images", fileName);
					var absolutePath = Path.Combine(_env.WebRootPath, relativePath);

					using (FileStream stream = new FileStream(absolutePath, FileMode.Create))
					{
						file.CopyTo(stream);
					}

				}

				content = new Content
				{
					ID = contentView.ID,
					ContentTitle = contentView.ContentTitle,
					ShortDescription = contentView.ShortDescription,
					ImageUrl = relativePath,
					LongDescription = contentView.LongDescription,
					NoOfLikes = contentView.NoOfLikes,
					IsEdited = contentView.IsEdited,
					IsHeadline = contentView.IsHeadline,
					DatePublished = DateTime.Now,
					DateEdited = DateTime.Now,
					VideoUrl = contentView.VideoUrl,
					CategoryID = contentView.CategoryID
				};
				

                _context.Add(content);
				
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_context.Category, "ID", "CategoryName", content.CategoryID);
            return View(content);
        }

		// GET: Contents/Edit/5
	
		public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var content = await _context.Content.FindAsync(id);
            if (content == null)
            {
                return NotFound();
            }
			ContentCreateViewModel realContent = new ContentCreateViewModel { Category = content.Category, CategoryID = content.CategoryID, ContentTitle = content.ContentTitle, DateEdited = content.DateEdited, DatePublished = content.DatePublished,
																			  ID = content.ID, ImageUrl = content.ImageUrl, IsEdited = content.IsEdited, IsHeadline = content.IsHeadline, LongDescription = content.LongDescription,
																			  NoOfLikes = content.NoOfLikes, ShortDescription = content.ShortDescription, VideoUrl = content.VideoUrl};
            ViewData["CategoryID"] = new SelectList(_context.Category, "ID", "CategoryName", content.CategoryID);
            return View(realContent);
        }

		// POST: Contents/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
	
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ContentTitle,ShortDescription,Url,LongDescription,NoOfLikes,IsEdited,IsHeadline,DatePublished,DateEdited,CategoryID,VideoUrl,ImageUrl")] ContentCreateViewModel contentView)
        {
			Content content = new Content();
            if (id != contentView.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
				IFormFile file = contentView.Url;
				string relativePath = "";
				if (file != null)
				{
					if (contentView.ImageUrl != null)
					{
						var oldPath = Path.Combine(_env.WebRootPath, contentView.ImageUrl);
						if (System.IO.File.Exists(oldPath))
						{
							System.IO.File.Delete(oldPath);
						}
					}
					
					var fileName = $"{contentView.ID}{Path.GetFileName(file.FileName)}";
					relativePath = Path.Combine("images", fileName);
					var absolutePath = Path.Combine(_env.WebRootPath, relativePath);

					using (FileStream stream = new FileStream(absolutePath, FileMode.Create))
					{
						file.CopyTo(stream);
					}

				}
				content = new Content
				{
					ID = contentView.ID,
					ContentTitle = contentView.ContentTitle,
					ShortDescription = contentView.ShortDescription,
					ImageUrl = (!string.IsNullOrEmpty(relativePath)) ? relativePath : contentView.ImageUrl,
					LongDescription = contentView.LongDescription,
					NoOfLikes = contentView.NoOfLikes,
					IsEdited = contentView.IsEdited,
					IsHeadline = contentView.IsHeadline,
					DatePublished = contentView.DatePublished,
					DateEdited = DateTime.Now,
					VideoUrl = contentView.VideoUrl,
					CategoryID = contentView.CategoryID
				};
				try
                {
                    _context.Update(content);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContentExists(content.ID))
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
            ViewData["CategoryID"] = new SelectList(_context.Category, "ID", "ID", content.CategoryID);
            return View(contentView);
        }

		// GET: Contents/Delete/5
		
		public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var content = await _context.Content
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (content == null)
            {
                return NotFound();
            }

            return View(content);
        }

		// POST: Contents/Delete/5
	
		[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var content = await _context.Content.FindAsync(id);
			if (string.IsNullOrEmpty(content.ImageUrl))
			{
				content.ImageUrl = "";
			}
			var oldPath = Path.Combine(_env.WebRootPath, content.ImageUrl);
			if (System.IO.File.Exists(oldPath))
			{
				System.IO.File.Delete(oldPath);
			}
			_context.Content.Remove(content);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContentExists(int id)
        {
            return _context.Content.Any(e => e.ID == id);
        }
    }
}
