using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Week15Project.Models;
using Week15Project.Models.ViewModels;
using Week15Project.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IForumRepository, ForumRepository>();

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Week15Project")));

builder.Services.AddTransient<IRoomProfiler, RoomProfiler>();
builder.Services.AddTransient<IUserProfiler, UserProfiler>();

builder.Services.AddHttpClient();

var app = builder.Build();

//Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

using (var scope = app.Services.CreateScope())
{
    var userDBContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    //userDBContext.Database.EnsureDeleted();
    userDBContext.Database.EnsureCreated();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
