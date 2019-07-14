using Lawyer.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Lawyer.Configuration 
{
	public class UserConfiguration : EntityTypeConfiguration<User>
	{
		public UserConfiguration()
		{
			Property(p => p.Name)
				.IsVariableLength()
				.IsUnicode()
				.IsRequired();

			Property(x => x.RegisterDate)
				.HasColumnType("date")
				.IsRequired();

			HasMany(x => x.Asks)
				.WithRequired(x => x.Author)
				.WillCascadeOnDelete(false);

			HasMany(x => x.Answers)
				.WithRequired(x => x.Author)
				.WillCascadeOnDelete(false);
		}
	}
}