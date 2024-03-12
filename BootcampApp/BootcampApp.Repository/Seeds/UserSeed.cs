using BootcampApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BootcampApp.Repository.Seeds
{
    public class UserSeed : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
               new User() { UserName = "UmutBey" },
               new User() { UserName = "AhmetKayaHoca" }

               );
        }
    }
}
