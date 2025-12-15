using AutoMapper;
using Courses.Application.Interfaces.RepositoryInterfaces;
using Courses.Application.Interfaces.SecurityInterfaces;
using Courses.Application.Mappers.UserMapper;
using Courses.Application.Services.Email;
using Courses.Application.Services.Security.PasswordHash;
using Courses.Application.Services.ServiceInterfaces;
using Courses.Application.Services.UserService;
using Courses.Infrastructure.Context;
using Courses.Infrastructure.Repositories.UserRepo;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region AddAuthentication
builder.Services.AddAuthentication(
    options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultForbidScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    }).AddCookie(options=>
    {
        options.LoginPath = "/Account/SignIn";
        options.LogoutPath = "/Account/SignOut";
        options.AccessDeniedPath = "/Home/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromDays(30);
    });
#endregion

#region Add UserRepository
builder.Services.AddScoped<IUserRepository, UserRepository>();
#endregion

#region Add UserService
builder.Services.AddScoped<IUserServices, UserServices>();
#endregion

#region Add DbContext
builder.Services.AddDbContext<CoursesDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});
#endregion

#region Add AutoMapper
builder.Services.AddAutoMapper(cfg =>
{
    
}, typeof(UserMappingProfile).Assembly);
#endregion

#region Add PasswordHasher
builder.Services.AddTransient<IPasswordHasher, IdentityPasswordHasher>();
#endregion

#region Add EmailSettings
builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
