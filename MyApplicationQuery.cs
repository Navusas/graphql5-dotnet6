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
        Name = "Query";

        //Field<ConsumerType>().Name("consumer")
        //   .Resolve()
        //   .WithScope() // creates a service scope as described above; not necessary for serial execution
        //   .WithService<AppDbContext>()
        //   .Resolve((context, db) => db.Consumers.FirstOrDefault(x => x.FirstName.Equals("Hello World")));

        FieldAsync<ListGraphType<ConsumerType>, IEnumerable<Consumer>>(
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
    }
}