using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UturuAbiaNews.Models
{
    public class Category
    {
		public int ID { get; set; }
		[Display(Name = "Category Name")]
		public string CategoryName { get; set; }

		[Display(Name = "Featured")]
		public bool IsFeautured { get; set; }
		public ICollection<Content> Contents { get; set; }
		public ICollection<Advertisement> Advertisements { get; set; }
	}
}
