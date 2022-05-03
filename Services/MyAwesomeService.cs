using GraphQLAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraphQLAPI;

public class MyAwesomeService : IMyAwesomeService
{
    private readonly AppDbContext _dbContext;

    public MyAwesomeService(AppDbContext dbContext) {
        _dbContext = dbContext;
    }

    public Consumer? GetByName(string name) {
        return _dbContext.Consumers.FirstOrDefault(x => x.FirstName.Equals(name));
    }
    public async Task<IEnumerable<Consumer>> GetAll()
    {
        return await _dbContext.Consumers.ToListAsync();
    }
}