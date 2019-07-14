using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lawyer.Entities
{
	public class Answer
	{
		public Guid Id { get; set; }

		public string Text { get; set; }

		public User Author { get; set; }

		public Ask Ask { get; set; }

		public DateTime Date { get; set; }

		public bool Confirmation { get; set; }
	}
}