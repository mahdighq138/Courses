using Courses.Application.Mappers.UserMapper;
using Courses.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Courses.Domain.Interfaces.SecurityInterfaces;
using Courses.Application.Services.Security.PasswordHash;
using Courses.Application.Services.ServiceInterfaces;
using Courses.Application.Services.UserService;
using Courses.Domain.Interfaces.RepositoryInterfaces;
using Courses.Infrastructure.Repositories.UserRepo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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
