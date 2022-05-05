using GraphQL;
using GraphQL.MicrosoftDI;
using GraphQL.Types;
using GraphQLAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraphQLAPI;

public class MyApplicationQuery : ObjectGraphType<object>
{
    public MyApplicationQuery()
    {
        Name = "NoIdeaWhereThisIsBeingUsed";

        // This is just an example of how to construct the same query, but just using builder method
        // instead of the <some other> method 
        //Field<ConsumerType>().Name("consumer")
        //   .Resolve()
        //   .WithScope() // creates a service scope as described above; not necessary for serial execution
        //   .WithService<AppDbContext>()
        //   .Resolve((context, db) => db.Consumers.FirstOrDefault(x => x.FirstName.Equals("Hello World")));

        FieldAsync<ListGraphType<ConsumerType>, IEnumerable<Customer>>(
            "consumers",
            resolve: async context =>
            {
                using var scope = context.RequestServices.CreateScope();
                var services = scope.ServiceProvider;
                return await services.GetRequiredService<IMyAwesomeService>().GetAll();
            }
            );

        Field<ConsumerType>(
            "consumer",
            resolve: context =>
            {
                using var scope = context.RequestServices.CreateScope();
                var services = scope.ServiceProvider;
                return services.GetRequiredService<IMyAwesomeService>().GetAll().Result.First();
            }
        );

        FieldAsync<ListGraphType<VirtualBookType>, IEnumerable<VirtualBook>>(
            "virtualBooks",
            resolve: async context =>
            {
                using var scope = context.RequestServices.CreateScope();
                var services = scope.ServiceProvider;
                return await services.GetRequiredService<IMyAwesomeService>().GetAllVirtualBooks();
            }
            );
    }
}