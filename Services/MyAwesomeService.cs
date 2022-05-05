using GraphQLAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraphQLAPI;

public class MyAwesomeService : IMyAwesomeService
{
    private readonly AppDbContext _dbContext;

    public MyAwesomeService(AppDbContext dbContext) {
        _dbContext = dbContext;
    }

    public Customer? GetByName(string name) {
        return _dbContext.Consumers.FirstOrDefault(x => x.FirstName.Equals(name));
    }
    public async Task<IEnumerable<Customer>> GetAll()
    {
        return await _dbContext.Consumers.ToListAsync();
    }

    public async Task<IEnumerable<VirtualBook>> GetAllVirtualBooks()
    {
        // Yes, you have to do .Include(), otherwise it will be null
        return await _dbContext.VirtualBooks.Include(c => c.PurchasedBy).ToListAsync();
    }
}