using GraphQLAPI.Entities;

namespace GraphQLAPI;

public interface IMyAwesomeService
{
    Consumer? GetByName(string name);
    Task<IEnumerable<Consumer>> GetAll();
}