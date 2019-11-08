using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UturuAbiaNews.Models
{
	public class MockAdvertisement
	{
		public int ID { get; set; }
		public string AdvertName { get; set; }
		public string AdvertDescription { get; set; }
		public string AdvertImage { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public int Position { get; set; }
		public int CategoryID { get; set; }
	}
}
