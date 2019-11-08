using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UturuAbiaNews.Models
{
	public class MobileViewModel
	{
		public ICollection<MockCategory> Categories { get; set; }
		public ICollection<MockContent> Contents { get; set; }
		public ICollection<MockAdvertisement> Advertisements { get; set; }
	}

	public class MainItem
	{
		public MockContent Content { get; set; }
		public ICollection<MockContent> Contents { get; set; }
	}
}
