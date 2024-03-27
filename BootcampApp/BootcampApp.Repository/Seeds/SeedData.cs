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


                if (!context!.Categories.Any())
                {
                    context.Categories.AddRange(
                        new Category() { Text = "Elekronik", Url = "elektronik", Color = CategoryColor.primary },
                        new Category { Text = "Gıda", Url = "gida", Color = CategoryColor.success },
                        new Category { Text = "Kitap", Url = "kitap", Color = CategoryColor.danger },
                        new Category { Text = "Yazılım", Url = "yazilim", Color = CategoryColor.secondary },
                        new Category { Text = "Müzik", Url = "muzik", Color = CategoryColor.warning }
                    );
                    context.SaveChanges();
                }
                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                        new User()
                        {
                            UserName = "UmutBey",
                            Picture = "avatar1.png",
                            Email = "umutbey@gmail.com",
                        },
                          new User()
                          {
                              UserName = "AhmetKayaHoca",
                              Picture = "avatar2.png",
                              Email = "info@ahmetkaya.com",
                          }
                   );
                    context.SaveChanges();
                }

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
                            UserId = context.Users.First().Id,
                            LikeCount = 4,
                            Categories = context.Categories.Take(2).ToList(),
                            Comments = new List<Comment>{
                            new Comment {Text = "Güzel bootcamp",PublishedDate = new DateTime(),UserId = context.Users.First().Id,},
                            new Comment {Text = "Katılmayı düşünüyorum",PublishedDate = new DateTime(),UserId = context.Users.Skip(1).First().Id,},
                        }

                        },
                        new Post
                        {
                            Title = "Unity İle geliştirmek ne kadar zor",
                            Content = "Unity ile oyun yapımaya çalışıyrum durmadan hata alıyorum",
                            Image = "unity.png",
                            IsActive = true,
                            PublishedDate = DateTime.Now.AddDays(-7),
                            LikeCount = 2,
                            UserId = context.Users.First().Id,
                            Categories = context.Categories.Take(2).ToList(),
                        },
                        new Post
                        {
                            Title = "Yazılım ile Müzik",
                            Content = "Yazılım ile müzik geliştirmeyi nasıl yapıyorsunuz başım ağrıyor kod yazarken",
                            Image = "yazilimMuzik.jpg",
                            LikeCount = 1,
                            IsActive = true,
                            PublishedDate = DateTime.Now.AddDays(-10),
                            UserId = context.Users.Where(x => x.UserName!.Contains("AhmetKayaHoca")).First().Id,
                            Categories = context.Categories.Take(2).ToList(),
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
