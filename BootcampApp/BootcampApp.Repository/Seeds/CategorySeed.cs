using BootcampApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BootcampApp.Repository.Seeds
{
    public class CategorySeed : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category() { Text = "Elekronik" },
                new Category() { Text = "Gıda" },
                new Category() { Text = "Kitap" },
                new Category() { Text = "Yazılım" },
                new Category() { Text = "Müzik" }
                );
        }
    }
}
