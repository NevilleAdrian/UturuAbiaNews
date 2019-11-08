using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UturuAbiaNews.Data
{
	public class ApplicationUser: IdentityUser
	{
		public bool IsAdmin { get; set; }
		public bool IsModerator { get; set; }
		public bool IsUploader { get; set; }
	}
}
