using System;
using System.Collections.Generic;

namespace Lawyer.Entities
{
	public class Ask
	{
		public Guid Id { get; set; }

		public string Slug { get; set; }

		public string Title { get; set; }

		public string Body { get; set; }

		public DateTime Date { get; set; }

		public User Author { get; set; }

		public bool IsAnswered { get; set; } 

		public ICollection<Answer> Answers { get; set; }
	}
}