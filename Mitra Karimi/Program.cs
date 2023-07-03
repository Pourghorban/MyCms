using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Mitra_Karimi.Context;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var app = builder.Build();

var contentRootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
var configurationBuilder = new ConfigurationBuilder()
    .SetBasePath(contentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
var configuration = configurationBuilder.Build();

var connectionString = configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MyCmsContext>(options => options.UseSqlServer(connectionString));


#region Repository
builder.Services.AddScoped<IPageCommentRepository, PageCommentRepository>();
builder.Services.AddScoped<IPageRepository, PageRepository>();
builder.Services.AddScoped<IPageGroupRepository, PageGroupRepository>();
builder.Services.AddScoped<AdminLogin>();


#endregion


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