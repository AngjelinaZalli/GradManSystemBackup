using GradManSystem1.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddRoles<IdentityRole>()
//    .AddEntityFrameworkStores<ApplicationDbContext>();

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddRoles<IdentityRole>()
    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();
AddAuthorizationPolicies();

//builder.Services.AddAuthorization(options =>
//{
//    options.FallbackPolicy = new AuthorizationPolicyBuilder()
//    .RequireAuthenticatedUser()
//    .Build();
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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

void AddAuthorizationPolicies()
{
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("Student", x => x.RequireClaim("Student"));
    });

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("Admin", x => x.RequireRole("Admin"));
        options.AddPolicy("Editor", x => x.RequireRole("Editor"));
        options.AddPolicy("Student", x => x.RequireRole("Student"));
    });
}