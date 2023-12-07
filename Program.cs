using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("TaskManagementContextConnection") ?? throw new InvalidOperationException("Connection string 'TaskManagementContextConnection' not found.");

builder.Services.AddDbContext<TaskManagementContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<TaskManageUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<TaskManagementContext>();

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
