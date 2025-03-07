using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

// NOT AUTOGENERATED
public class TelecamContext : DbContext
{
    public TelecamContext(DbContextOptions<TelecamContext> options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<Contact>().ToTable("Contacts");
        modelBuilder.Entity<Message>().ToTable("Messages");

        base.OnModelCreating(modelBuilder);
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Message> Messages { get; set; }
    
}