using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UturuAbiaNews.Data;
using UturuAbiaNews.Models;

namespace UturuAbiaNews.Controllers
{
    public class UturuMobileController : Controller
    {
		private readonly ApplicationDbContext _context;
		public UturuMobileController(ApplicationDbContext context)
		{
			_context = context;
		}
		// GET: UturuMobile
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			MobileViewModel mobileView = new MobileViewModel
			{
				Categories = new List<MockCategory>(),
				Contents = new List<MockContent>(),
				Advertisements = new List<MockAdvertisement>()
			};
			var navigation = await _context.Category.Include(c => c.Contents).Include(a => a.Advertisements).ToListAsync();
			var contents = navigation.FirstOrDefault().Contents.ToList();
			var advertisments = navigation.FirstOrDefault().Advertisements.ToList();
			foreach (var nav in navigation)
			{
				mobileView.Categories.Add(new MockCategory { CategoryName = nav.CategoryName, ID = nav.ID });
			}
			foreach (var content in contents)
			{
				mobileView.Contents.Add(new MockContent
				{
					ID = content.ID,
					CategoryID = content.CategoryID,
					ContentTitle = content.ContentTitle,
					DateEdited = content.DateEdited,
					DatePublished = content.DatePublished,
					ImageUrl = $"https://www.uturuabianews.com/{content.ImageUrl}",
					LongDescription = content.LongDescription,
					NoOfComments = content.NoOfComments,
					NoOfLikes = content.NoOfLikes,
					NoOfViews = content.NoOfViews,
					ShortDescription = content.ShortDescription,
					VideoUrl = content.VideoUrl
				});
			}
			foreach (var advert in advertisments)
			{
				var duration = advert.EndDate - DateTime.Now;
				if (DateTime.Now <= advert.EndDate && duration > TimeSpan.Zero)
				{
					mobileView.Advertisements.Add(new MockAdvertisement
					{
						ID = advert.ID,
						AdvertDescription = advert.AdvertDescription,
						AdvertImage = advert.AdvertImage,
						AdvertName = advert.AdvertName,
						CategoryID = advert.CategoryID
					});
				}
			}

			
			
			return Json(mobileView);
		}

		// GET: UturuMobile/5
		[HttpGet]
		public async Task<IActionResult> GetWithId(int id)
		{
			MobileViewModel mobileView = new MobileViewModel
			{
				Categories = new List<MockCategory>(),
				Contents = new List<MockContent>(),
				Advertisements = new List<MockAdvertisement>()
			};

			var navigation = await _context.Category.Include(c => c.Contents).Include(a => a.Advertisements).ToListAsync();
			var contents = navigation.Where(i => i.ID == id).FirstOrDefault().Contents.ToList();
			var advertisments = navigation.Where(i => i.ID == id).FirstOrDefault().Advertisements.ToList();
			if (contents != null)
			{
				foreach (var nav in navigation)
				{
					mobileView.Categories.Add(new MockCategory { CategoryName = nav.CategoryName, ID = nav.ID });
				}
				foreach (var content in contents)
				{
					mobileView.Contents.Add(new MockContent {
						ID = content.ID, CategoryID = content.CategoryID,
						ContentTitle = content.ContentTitle, DateEdited = content.DateEdited,
						DatePublished = content.DatePublished, ImageUrl = $"https://www.uturuabianews.com/{content.ImageUrl}",
						LongDescription = content.LongDescription, NoOfComments = content.NoOfComments,
						NoOfLikes = content.NoOfLikes, NoOfViews = content.NoOfViews,
						ShortDescription = content.ShortDescription, VideoUrl = content.VideoUrl
					});
				}
				foreach (var advert in advertisments)
				{
					var duration = advert.EndDate - DateTime.Now;
					if (DateTime.Now <= advert.EndDate && duration > TimeSpan.Zero)
					{
						mobileView.Advertisements.Add(new MockAdvertisement{
							ID = advert.ID, AdvertDescription = advert.AdvertDescription,
							AdvertImage = advert.AdvertImage, AdvertName = advert.AdvertName,
							CategoryID = advert.CategoryID
						});
					}
				}
			}
				

			
			return Json(mobileView);
		}

		// GET: UturuMobile/5
		[HttpGet]
		public async Task<IActionResult> GetItemWithId(int id)
		{
			

			var content = await _context.Content.Where(i => i.ID == id).Include(c => c.Category.Contents).FirstOrDefaultAsync();
			MainItem mobileView = new MainItem
			{
				Content = new MockContent {
					ID = content.ID,
					CategoryID = content.CategoryID,
					ContentTitle = content.ContentTitle,
					DateEdited = content.DateEdited,
					DatePublished = content.DatePublished,
					ImageUrl = $"https://www.uturuabianews.com/{content.ImageUrl}",
					LongDescription = content.LongDescription,
					NoOfComments = content.NoOfComments,
					NoOfLikes = content.NoOfLikes,
					NoOfViews = content.NoOfViews,
					ShortDescription = content.ShortDescription,
					VideoUrl = content.VideoUrl
				}
				
			};
			mobileView.Contents = new List<MockContent>();
			foreach (var item in content.Category.Contents)
			{
				var mockContent = new MockContent
				{
					ID = item.ID,
					CategoryID = item.CategoryID,
					ContentTitle = item.ContentTitle,
					DateEdited = item.DateEdited,
					DatePublished = item.DatePublished,
					ImageUrl = $"https://www.uturuabianews.com/{item.ImageUrl}",
					LongDescription = item.LongDescription,
					NoOfComments = item.NoOfComments,
					NoOfLikes = item.NoOfLikes,
					NoOfViews = item.NoOfViews,
					ShortDescription = item.ShortDescription,
					VideoUrl = item.VideoUrl
				};
				mobileView.Contents.Add(mockContent);
			}
			return Json(mobileView);
		}
	}
}