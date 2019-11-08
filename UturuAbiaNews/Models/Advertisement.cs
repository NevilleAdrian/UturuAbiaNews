using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UturuAbiaNews.Models
{
	public class Advertisement
	{
		public int ID { get; set; }
		[Display(Name = "Advert Name")]
		public string AdvertName { get; set; }

		[Display(Name = "Advert Description")]
		public string AdvertDescription { get; set; }

		public string AdvertImage { get; set; }

		[Display(Name = "Start Date")]
		public DateTime StartDate { get; set; }

		[Display(Name = "End Date")]
		public DateTime EndDate { get; set; }

		[Display(Name = "Position")]
		public int Position { get; set; }

		public Category Category { get; set; }
		public int CategoryID { get; set; }
	}
}
