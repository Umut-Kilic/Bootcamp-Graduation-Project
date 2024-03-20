using Autofac;
using Autofac.Extensions.DependencyInjection;
using BootcampApp.Core.Models;
using BootcampApp.Core.Services;
using BootcampApp.Repository;
using BootcampApp.Repository.Seeds;
using BootcampApp.Service.Mapping;
using BootcampApp.Service.Services;
using BootcampApp.Web.Extenisons;
using BootcampApp.Web.Filters;
using BootcampApp.Web.Modules;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<PostViewModelValidator>());

builder.Services.AddDbContext<BootcampAppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"));
});

//Security eklemem�n sebebi kullan�c� kritik bilgilerde g�ncelleme yyapt�g�nda 30 dkda bir bakarak kullan�c�n login sayfas�na yonlend�r�lmes� di�er t�rl�
//kullan�c� ba�ka c�hazdan b�lg�ler�n� guncellese bile cookie omru kadar eski b�lg�ler� gorurdurk.
builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{
    options.ValidationInterval=TimeSpan.FromMinutes(30);
});


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
    options.ExpireTimeSpan = TimeSpan.FromDays(15);

    //Kullan�c� her giri� yaptugunda �mrunu otamat�k yen�ler
    options.SlidingExpiration = true;


});


//Identity

builder.Services.AddIdentityWithExt();



var app = builder.Build();

SeedData.Initialize(app);

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
    name: "areas",
   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
