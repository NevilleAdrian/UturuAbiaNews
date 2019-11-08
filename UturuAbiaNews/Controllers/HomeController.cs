using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UturuAbiaNews.Data;
using UturuAbiaNews.Models;

namespace UturuAbiaNews.Controllers
{
	public class HomeViewModel
	{
		
		public ICollection<Content> Headlines { get; set; }
		public ICollection<Category> Categories { get; set; }
		public ICollection<Advertisement> TopRightAdvertisements { get; set; }
		public ICollection<Advertisement> BottomRightAdvertisements { get; set; }
		public ICollection<Advertisement> BottomAdvertisements { get; set; }
		public ICollection<Content> Trending { get; set; }
		public ICollection<Content> Videos { get; set; }

	}
    public class HomeController : Controller
    {
		private ApplicationDbContext _context;
		public HomeController(ApplicationDbContext context)
		{
			_context = context;
		}
        public IActionResult Index()
        {
			List<Content> contents = _context.Content.Include(c => c.Category).ToList();
			List<Category> categories = _context.Category.Include(b => b.Advertisements).ToList();
			HomeViewModel catcont = new HomeViewModel();
			catcont.Headlines = new List<Content>();
			catcont.Categories = new List<Category>();
			catcont.TopRightAdvertisements = new List<Advertisement>();
			catcont.BottomRightAdvertisements = new List<Advertisement>();
			catcont.BottomAdvertisements = new List<Advertisement>();
			catcont.Trending = new List<Content>();
			catcont.Videos = new List<Content>();
			foreach (Content content in contents)
			{
				if (content.IsHeadline)
				{
					catcont.Headlines.Add(content);
				}

				else if (content.Category.IsFeautured == true)
				{
					catcont.Categories.Add(content.Category);



				}
			}

			foreach (var category in categories)
			{
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
			}

			foreach (var trending in contents)
			{
				if (trending.NoOfViews >= 2)
				{
					catcont.Trending.Add(trending);
				}
				if (!string.IsNullOrEmpty(trending.VideoUrl))
				{
					catcont.Videos.Add(trending);
				}	
			}
			return View(catcont);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
