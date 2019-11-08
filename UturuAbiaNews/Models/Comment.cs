using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UturuAbiaNews.Models
{
	public class Comment
	{
		public int ID { get; set; }
		public string Message { get; set; }

		[Required]
		[Display(Name = "Name")]
		public string UserName { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		public Content Content { get; set; }
		public int ContentID { get; set; }
	}
}
