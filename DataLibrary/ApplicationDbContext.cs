using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DataLibrary.Data.Repository;
using Contracts.Models.Articles;
using Contracts.Models.Comments;
using Contracts.Models.Tegs;
using Contracts.Models.Users;

namespace DataLibrary.Data;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureCreated();     
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseSqlite(@"Data Source =.\\Data\\SQlLiteDatabase.db");
    //}

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