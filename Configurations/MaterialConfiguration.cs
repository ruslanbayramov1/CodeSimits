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
                .WithOne(e => e.Materials)
                .HasForeignKey(e => e.MaterialsId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
