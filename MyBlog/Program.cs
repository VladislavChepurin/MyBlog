using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyBlog;
using MyBlog.Data;
using MyBlog.Data.Repositiry;
using MyBlog.Data.Repository;
using MyBlog.Data.UoW;
using MyBlog.Extentions;
using MyBlog.Models.Articles;
using MyBlog.Models.Users;
using MyBlog.Validation;
using SignalRChat.Hubs;


var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

builder.Services.AddRazorPages();
builder.Services.AddSignalR();

// Add services to the container.
services.AddControllersWithViews();

services.AddValidatorsFromAssemblyContaining<ArticleValidator>(); // register validators
services.AddScoped<IValidator<Article>, ArticleValidator>();

services.AddTransient<IUnitOfWork, UnitOfWork>();

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connection))
    .AddUnitOfWork()
    .AddCustomRepository<Article, ArticleRepository>()
    .AddCustomRepository<Comment, CommentRepository>()
    .AddCustomRepository<Teg, TegRepository>()
    .AddIdentity<User, IdentityRole>(opts =>
    {
        opts.Password.RequiredLength = 5;
        opts.Password.RequireNonAlphanumeric = false;
        opts.Password.RequireLowercase = false;
        opts.Password.RequireUppercase = false;
        opts.Password.RequireDigit = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>();

var mapperConfig = new MapperConfiguration((v) =>
{
    v.AddProfile(new MappingProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
services.AddSingleton(mapper);

services.AddScoped<IRoleRepository, RoleRepository>();

//Build services
var app = builder.Build();
//--------------------------------------------------
var webSocketOptions = new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromMinutes(2)
};
app.UseWebSockets(webSocketOptions);

//--------------------------------------------------
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
app.MapHub<DataHub>("/chatHub");

app.Run();






