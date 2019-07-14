using Lawyer.Configuration;
using Lawyer.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Lawyer.Context
{
	public class LawyerContext : IdentityDbContext<User>
	{
		public LawyerContext()
		: base("name=LawyerContext")
		{
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.Add(new AskConfiguration());
			modelBuilder.Configurations.Add(new UserConfiguration());
			modelBuilder.Configurations.Add(new AnswerConfiguration());
			base.OnModelCreating(modelBuilder);
		}
		public DbSet<Ask> Asks { get; set; }
		public DbSet<Answer> Answers { get; set; }
	}
}