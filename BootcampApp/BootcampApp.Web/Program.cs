using Autofac;
using Autofac.Extensions.DependencyInjection;
using BootcampApp.Repository;
using BootcampApp.Repository.Seeds;
using BootcampApp.Service.Mapping;
using BootcampApp.Web.Filters;
using BootcampApp.Web.Modules;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using NLayerApp.Service.Validations;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<PostViewModelValidator>());

builder.Services.AddDbContext<BootcampAppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"));
});

builder.Services.AddAutoMapper(typeof(MapProfile));
builder.Services.AddScoped(typeof(NotFoundFilter<>));


builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new RepoServiceModule()));



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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
