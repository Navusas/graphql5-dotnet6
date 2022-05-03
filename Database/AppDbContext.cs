using GraphQLAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraphQLAPI;

 public class AppDbContext : DbContext, IAppDbContext
 {
     public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {

     }

     protected override void OnModelCreating(ModelBuilder builder) {
         base.OnModelCreating(builder);
     }

     public DbSet<Consumer> Consumers {get;set;}
 }