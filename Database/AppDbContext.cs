using GraphQLAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraphQLAPI;

 public class AppDbContext : DbContext, IAppDbContext
 {
     public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {

     }

     protected override void OnModelCreating(ModelBuilder builder) {
         base.OnModelCreating(builder);

        builder.Entity<VirtualBook>()
            .HasMany(c => c.PurchasedBy)
            .WithOne();

        builder.UsePropertyAccessMode(PropertyAccessMode.Field);
     }

     public DbSet<Customer> Consumers {get;set;}
    public DbSet<VirtualBook> VirtualBooks {get;set;}
 }