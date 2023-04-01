using BissnesLibrary.ControllerServices.Interface;
using BissnesLibrary.ControllerServices;
using Microsoft.OpenApi.Models;
using DataLibrary.Data.UoW;
using BissnesLibrary.ContextServices.Interface;
using BissnesLibrary.ContextServices;
using Contracts.Models.Articles;
using Contracts.Models.Comments;
using Contracts.Models.Tegs;
using Contracts.Models.Users;
using DataLibrary.Data.Repository;
using DataLibrary.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyBlog.Extentions;

namespace API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var services = builder.Services;


        services.AddScoped<IUnitOfWork, UnitOfWork>();
       // services.AddScoped<IArticleService, ArticleService>();
        services.AddScoped<ITegService, TegService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IEditService, EditService>();
        services.AddScoped<IAdminService, AdminService>();
        services.AddTransient<IUserResolverService, UserResolverService>(); //очень часто вызывается, лучше держать в памяти постояннно
        services.AddScoped<ISingInResolverService, SingInResolverService>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
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

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyBlog", Version = "v1" });
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HomeAPI v1"));
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}