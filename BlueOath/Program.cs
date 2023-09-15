using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BlueOath.Data;
using Microsoft.AspNetCore.Identity;
using BlueOath.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BlueOath.Data.BlueOathContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BlueOathContext") ?? throw new InvalidOperationException("Connection string 'BlueOathContext' not found.")));
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<BlueOath.Areas.Identity.Data.BlueOathContext>();builder.Services.AddDbContext<BlueOath.Areas.Identity.Data.BlueOathContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BlueOathContext")));
// Add services to the container.
builder.Services.AddControllersWithViews();

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
