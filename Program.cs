using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using salesWebMvc.Data;
using salesWebMvc.Services;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<salesWebMvcContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("salesWebMvcContext"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("salesWebMvcContext"))));

builder.Services.AddTransient<SeedingService>();
builder.Services.AddScoped<SellerService>();
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

//Seed Data
void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<SeedingService>();
        service.Seed();
    }
}

SeedData(app);
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
