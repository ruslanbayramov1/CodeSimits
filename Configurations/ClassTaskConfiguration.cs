using CodeSimits.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeSimits.Configurations
{
    public class ClassTaskConfiguration : IEntityTypeConfiguration<ClassTask>
    {
        public void Configure(EntityTypeBuilder<ClassTask> builder)
        {
            builder.HasMany(e => e.Grades)
                .WithOne(e => e.ClassTask)
                .HasForeignKey(e => e.ClassTaskId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
