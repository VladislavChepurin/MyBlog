using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyBlog.Data.Repository;
using MyBlog.Models.Articles;
using MyBlog.Models.Comments;
using MyBlog.Models.Tegs;
using MyBlog.Models.Users;

namespace MyBlog.Data;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureCreated();     
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ArticleConfiguration());
        builder.ApplyConfiguration(new CommentConfiguration());
        builder.ApplyConfiguration(new TegConfiguration());    
    }

    public DbSet<Article>? Articles { get; set; }
    public DbSet<Teg>? Tegs { get; set; }
    public DbSet<Comment>? Comments { get; set; }
}