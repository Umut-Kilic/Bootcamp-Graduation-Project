using Autofac;
using Autofac.Extensions.DependencyInjection;
using BootcampApp.Core.Models;
using BootcampApp.Core.Services;
using BootcampApp.Repository;
using BootcampApp.Service.Mapping;
using BootcampApp.Service.Services;
using BootcampApp.Web.Extenisons;
using BootcampApp.Web.Filters;
using BootcampApp.Web.Modules;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BootcampAppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"));
});

builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{
    options.ValidationInterval = TimeSpan.FromMinutes(30);
});

builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Directory.GetCurrentDirectory()));

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddAutoMapper(typeof(MapProfile));
builder.Services.AddScoped(typeof(NotFoundFilter<>));
builder.Services.AddScoped<IEmailService, EmailService>();


builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new RepoServiceModule()));


builder.Services.ConfigureApplicationCookie(options =>
{
    var cookieBuilder = new CookieBuilder();
    cookieBuilder.Name = "BootcampAppCookie";

    options.LoginPath = new PathString("/Home/Index");
    options.LogoutPath = new PathString("/Member/logout");
    options.AccessDeniedPath = new PathString("/Member/AccessDenied");
    options.ExpireTimeSpan = TimeSpan.FromDays(15);

    options.SlidingExpiration = true;


});


builder.Services.AddIdentityWithExt();



var app = builder.Build();


app.UseExceptionHandler("/Home/Error");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "post_details",
    pattern: "posts/details/{postId}",
    defaults: new { controller = "Posts", action = "Details" }
);
app.MapControllerRoute(
    name: "posts_by_category",
    pattern: "posts/category/{categoryId}",
    defaults: new { controller = "Posts", action = "Index" }
);
app.MapControllerRoute(
    name: "user_profile",
    pattern: "profile/{username}",
    defaults: new { controller = "Member", action = "Index" }
);


app.MapControllerRoute(
    name: "areas",
   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
