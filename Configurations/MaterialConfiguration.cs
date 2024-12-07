using CodeSimits.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeSimits.Configurations
{
    public class MaterialConfiguration : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.HasMany(e => e.ClassTasks)
                .WithOne(e => e.Material)
                .HasForeignKey(e => e.Material)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
