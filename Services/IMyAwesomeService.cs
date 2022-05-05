using GraphQLAPI.Entities;

namespace GraphQLAPI;

public interface IMyAwesomeService
{
    Customer? GetByName(string name);
    Task<IEnumerable<Customer>> GetAll();
    Task<IEnumerable<VirtualBook>> GetAllVirtualBooks();
}