using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UturuAbiaNews.Models
{
	public class MockContent
	{
		public int ID { get; set; }
		public string ContentTitle { get; set; }
		public string ShortDescription { get; set; }
		public string LongDescription { get; set; }
		public bool IsHeadline { get; set; }
		public int NoOfViews { get; set; }
		public string ImageUrl { get; set; }
		public int NoOfLikes { get; set; }
		public bool IsEdited { get; set; }
		public DateTime DatePublished { get; set; }
		public DateTime DateEdited { get; set; }
		public int NoOfComments { get; set; }
		public string VideoUrl { get; set; }
		public int CategoryID { get; set; }
	}
}
