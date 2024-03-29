﻿using Microsoft.EntityFrameworkCore;
using Contracts.Models.Articles;
using Contracts.Models.Comments;
using Contracts.Models.Tegs;

namespace DataLibrary.Data.Repositiry;

#nullable disable

public class Repository<T> : IRepository<T> where T : class
{
    protected DbContext _context;

    public DbSet<Article> Articles { get; set; }
    public DbSet<Teg> Tegs { get; set; }
    public DbSet<Comment> Comments { get; set; }

    public DbSet<T> Set
    {
        get;
        private set;
    }

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        var set = _context.Set<T>();
        set.Load();
        Set = set;

        var articles = context.Articles;
        articles?.Load();
        Articles = articles;

        var tegs = context.Tegs;
        tegs?.Load();
        Tegs = tegs;

        var comments = context.Comments;
        comments?.Load();
        Comments = comments;
    }

    public void Create(T item)
    {
        Set.Add(item);
    }

    public void Delete(T item)
    {
        Set.Remove(item);
    }

    public T Get(int id)
    {
        return Set.Find(id);
    }

    public IEnumerable<T> GetAll()
    {
        return Set;
    }

    public void Update(T item)
    {
        Set.Update(item);
    }
}
