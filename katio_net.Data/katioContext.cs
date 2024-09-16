using System;
using katio.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace katio_net.Data;

public class katioContext: DbContext
{
    public katioContext(DbContextOptions<katioContext> options) : base(options)
    {}
    public DbSet<Book> Books { get; set;} = null;
    public DbSet<Author> Author { get; set;} = null;
    public DbSet<User> User { get; set;} = null;
    public DbSet<AudioBooks> AudioBooks { get; set;} = null;
    public DbSet<Narrator> Narrator { get; set;} = null;
    public DbSet<Genres> Genres { get; set;} = null;




    


}