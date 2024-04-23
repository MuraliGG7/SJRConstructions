using GirlScoutCookieBoothManager.Infrastructure;
using GirlScoutCookieBoothManager.Core.Interfaces;
using GirlScoutCookieBoothManager.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GirlScoutCookieBoothManager.Web.Configurations;
using GirlScoutCookieBoothManager.Web.Services.EmailService;
using GirlScoutCookieBoothManager.Web.Services.User;
using GirlScoutCookieBoothManager.Web.CustomMiddlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "MySigninInfo";
    options.DefaultSignInScheme = "MySigninInfo";
    options.DefaultSignOutScheme = "MySigninInfo";
    options.DefaultChallengeScheme = "MySigninInfo";
})
.AddCookie("MySigninInfo", options =>
{
    // Configure cookie options here
    options.Cookie.Name = "Signininfo";
    // Other cookie configurations...
});

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
var emailConfig = builder.Configuration
                    .GetSection("EmailConfiguration")
                    .Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);
builder.Services.AddScoped<IEmailRepository, EmailRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

