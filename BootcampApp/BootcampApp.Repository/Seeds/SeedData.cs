using BootcampApp.Core.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace BootcampApp.Repository.Seeds
{
    public static class SeedData
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<BootcampAppDbContext>())
            {
                if (context == null)
                {
                    throw new Exception("BootcampAppDbContext is null.");
                }

                if (context != null)
                {
                    if (context.Database.GetPendingMigrations().Any())
                    {
                        context.Database.Migrate();
                    }
                }


                if (!context.Categories.Any())
                {
                    context.Categories.AddRange(
                         new Category() { Text = "Elekronik" },
                        new Category() { Text = "Gıda" },
                        new Category() { Text = "Kitap" },
                        new Category() { Text = "Yazılım" },
                        new Category() { Text = "Müzik" }
                    );
                    context.SaveChanges();
                }
                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                        new User()
                        {
                            UserName = "UmutBey",
                            Picture = "avatar1.png"
                        },
                          new User()
                          {
                              UserName = "AhmetKayaHoca",
                              Picture = "avatar2.png"
                          }
                   );
                    context.SaveChanges();
                }
                // DbContext içinde herhangi bir veri var mı kontrol edin
                if (!context.Posts.Any())
                {
                    context.Posts.AddRange(
                        new Post
                        {
                            Title = "Elektronik Şarj cıhazı",
                            Content = "Xiaomı şarj cihazı çalışmıyor",
                            Image = "xiaomisarj.jpg",
                            IsActive = true,
                            PublishedDate = DateTime.Now.AddDays(-5),
                            UserId = context.Users.First().Id
                        },
                        new Post
                        {
                            Title = "Unity İle geliştirmek ne kadar zor",
                            Content = "Unity ile oyun yapımaya çalışıyrum durmadan hata alıyorum",
                            Image = "unity.png",
                            IsActive = true,
                            PublishedDate = DateTime.Now.AddDays(-7),
                            UserId = context.Users.First().Id
                        },
                        new Post
                        {
                            Title = "Yazılım ile Müzik",
                            Content = "Yazılım ile müzik geliştirmeyi nasıl yapıyorsunuz başım ağrıyor kod yazarken",
                            Image = "yazilimMuzik.jpg",
                            IsActive = true,
                            PublishedDate = DateTime.Now.AddDays(-10),
                            UserId = context.Users.Where(x => x.UserName.Contains("AhmetKayaHoca")).Select(x => x.Id).FirstOrDefault()
                        }
                    ); ;

                    // Değişiklikleri veritabanına kaydet
                    context.SaveChanges();
                }

                if (!context.Comments.Any())
                {
                    context.Comments.AddRange(
                         new Comment()
                         {
                             LikeCount = 1,
                             PostId = 1,
                             UserId = context.Users.First().Id,
                             Text = "Bu nasıl yorum",
                             PublishedDate = DateTime.Now.AddDays(-1),
                         }
                    );
                    context.SaveChanges();
                }


            }
        }
    }
}
