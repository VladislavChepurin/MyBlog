using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyBlog;
using MyBlog.Data;
using MyBlog.Data.Repository;
using MyBlog.Data.UoW;
using MyBlog.Extentions;
using MyBlog.Models.Articles;
using MyBlog.Models.Comments;
using MyBlog.Models.Tegs;
using MyBlog.Models.Users;
using MyBlog.Services.ContextServices;
using MyBlog.Services.ContextServices.Interface;
using MyBlog.Services.ControllerServices;
using MyBlog.Services.ControllerServices.Interface;
using NLog;
using NLog.Web;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    var services = builder.Services;

    services.AddSwaggerGen();
    services.AddRazorPages();

    // Add services to the container.
    services.AddControllersWithViews();

    services.AddScoped<IUnitOfWork, UnitOfWork>();
    services.AddScoped<IArticleService, ArticleService>();
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

    var mapperConfig = new MapperConfiguration((v) =>
    {
        v.AddProfile(new MappingProfile());
    });
    IMapper mapper = mapperConfig.CreateMapper();
    services.AddSingleton(mapper);

    //Build services
    var app = builder.Build();
    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }
    else
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.Use(async (ctx, next) =>
    {
        await next();

        if (ctx.Response.StatusCode == 404 && !ctx.Response.HasStarted)
        {
            //Re-execute the request so the user gets the error page
            string? originalPath = ctx.Request.Path.Value;
            ctx.Items["originalPath"] = originalPath;
            ctx.Request.Path = "/Page404";
            await next();
        }
    });

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

}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    LogManager.Shutdown();
}