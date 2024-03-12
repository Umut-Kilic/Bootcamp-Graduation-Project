using BootcampApp.Core.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;

namespace BootcampApp.Repository.Seeds
{
    public class PostSeed : IEntityTypeConfiguration<Post>
    {
        private readonly IApplicationBuilder appBuilder;
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            var context = appBuilder.ApplicationServices.CreateScope().ServiceProvider.GetService<BootcampAppDbContext>();

            builder.HasData(
                new Post
                {
                    Title = "Elektronik Şarj cıhazı",
                    Content = "Xiaomı şarj cihazı çalışmıyor",
                    Image = "1.jpeg",
                    IsActive = true,
                    PublishedDate = DateTime.Now.AddDays(-5),
                    Categories = context.Categories.Take(1).ToList(),
                    UserId = 1
                },
                new Post
                {
                    Title = "Unity İle geliştirmek ne kadar zor",
                    Content = "Unity ile oyun yapımaya çalışıyrum durmadan hata alıyorum",
                    Image = "2.png",
                    IsActive = true,
                    PublishedDate = DateTime.Now.AddDays(-7),
                    Categories = context.Categories.Skip(3).Take(1).ToList(),
                    UserId = 1
                },
                new Post
                {
                    Title = "Yazılım ile Müzk",
                    Content = "Yazılım ile müzik geliştirmeyi nasıl yapıyorsunuz başım ağrıyor kod yazarken",
                    Image = "3.png",
                    IsActive = true,
                    PublishedDate = DateTime.Now.AddDays(-10),
                    Categories = context.Categories.Skip(3).Take(2).ToList(),
                    UserId = 2
                });
        }


    }
}
