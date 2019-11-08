using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UturuAbiaNews.Models
{
	public class Content
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

		[DataType("Date")]
		public DateTime DatePublished { get; set; }
		[DataType("Date")]
		public DateTime DateEdited { get; set; }

		[Display(Name = "Comment")]
		public int NoOfComments { get; set; }

		[Display(Name = "YouTube Url")]
		public string VideoUrl { get; set; }


		public Category Category { get; set; }
		public int CategoryID { get; set; }
		
		public ICollection<Comment> Comments { get; set; }
	}
}
