using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace Lawyer.Entities
{
	public class User : IdentityUser
	{
		public string Name { get; set; }

		public DateTime RegisterDate { get; set; }

		public ICollection<Ask> Asks { get; set; }

		public ICollection<Answer> Answers { get; set; }

		public User()
		{
			RegisterDate = DateTime.UtcNow;
		}
	}
}