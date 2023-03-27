using AspNetCoreHero.ToastNotification;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using WebArtsShop.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ArtsShopContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ArtsShopOnline")));
builder.Services.AddSingleton<HtmlEncoder>(HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.All }));
// Add services to the container.
builder.Services.AddNotyf(config => { config.DurationInSeconds = 3;config.IsDismissable = true;config.Position = NotyfPosition.TopRight; });
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation().AddNToastNotifyNoty(new NotyOptions
{
    ProgressBar = true,
    Timeout = 5000
});

var app = builder.Build();

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
app.UseNToastNotify();

//app.MapRazorPages();
app.MapControllerRoute(
	  name: "areas",
	  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
	);
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
