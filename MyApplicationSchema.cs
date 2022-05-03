using GraphQL.Types;
using GraphQLAPI;

public class MyApplicationSchema : Schema
{
    public MyApplicationSchema(IServiceProvider services) : base(services)
    {
        Query = services.GetRequiredService<MyApplicationQuery>();
    }
}