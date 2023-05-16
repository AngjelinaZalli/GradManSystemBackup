using GradManSystem1.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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


builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(2);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
}
);

//builder.Services.ConfigureApplicationCookie(options =>
//{
//    options.Cookie.Name = "AspNetCore.Identity.Application";
//    options.ExpireTimeSpan = TimeSpan.FromSeconds(5);
//    options.SlidingExpiration = true;
//});



//var services = builder.Services;
//var configuration = builder.Configuration;
//services.AddAuthentication().AddGoogle(options =>
//{
//    options.ClientId = configuration["Authentication:Google:ClientId"];
//    options.ClientSecret = configuration["Authentication:Google:ClientSecret"];
//});
//services.AddAuthentication().AddFacebook(options =>
//{
//    options.ClientId = configuration["Authentication:Facebook:ClientId"];
//    options.ClientSecret = configuration["Authentication:Facebook:ClientSecret"];
//});

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
app.UseSession();

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