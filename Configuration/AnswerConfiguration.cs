using Lawyer.Entities;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lawyer.Configuration
{
	public class AnswerConfiguration : EntityTypeConfiguration<Answer>
	{
		public AnswerConfiguration()
		{
			Property(p => p.Id)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
				.IsRequired();

			Property(p => p.Text)
				.IsVariableLength()
				.IsUnicode()
				.IsRequired();

			HasRequired(c => c.Author)
			   .WithMany(p => p.Answers)
			   .WillCascadeOnDelete(false);

			HasRequired(c => c.Ask)
				.WithMany(p => p.Answers)
				.WillCascadeOnDelete(false);

			Property(p => p.Date)
				.HasColumnType("datetime2")
				.HasPrecision(0)
				.IsRequired();

			Property(c => c.Confirmation)
				.HasColumnType("bit")
				.IsRequired();
		}
	}
}