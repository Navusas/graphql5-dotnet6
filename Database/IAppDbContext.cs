using GraphQLAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraphQLAPI;

 public interface IAppDbContext
 {
     DbSet<Customer> Consumers { get; }
 }