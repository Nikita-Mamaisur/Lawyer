using Lawyer.Entities;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lawyer.Configuration
{
	public class AskConfiguration : EntityTypeConfiguration<Ask>
	{
		public AskConfiguration()
		{
			HasKey(p => p.Id);

			Property(p => p.Id)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
				.IsRequired();

			Property(p => p.Slug)
				.HasMaxLength(50)
				.IsVariableLength()
				.IsUnicode(false)
				.IsRequired();

			Property(p => p.Title)
			  .HasMaxLength(100)
			  .IsVariableLength()
			  .IsUnicode()
			  .IsRequired();

			Property(p => p.Body)
				.IsVariableLength()
				.IsUnicode()
				.IsRequired();


			Property(p => p.Date)
				.HasColumnType("datetime2")
				.HasPrecision(0)
				.IsRequired();

			HasRequired(c => c.Author)
			   .WithMany(p => p.Asks)
			   .WillCascadeOnDelete(false);

			HasMany(c => c.Answers)
			.WithRequired(p => p.Ask)
			.WillCascadeOnDelete(false);

			Property(c => c.IsAnswered)
				.HasColumnType("bit")
				.IsOptional();
		}
	}
}