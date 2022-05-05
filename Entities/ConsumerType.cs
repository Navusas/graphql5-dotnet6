using GraphQL.Types;

namespace GraphQLAPI.Entities;

public class ConsumerType : ObjectGraphType<Customer> {
    public ConsumerType() {
        Name = "consumer";

        Description = "Represents the consumer using our services";

        Field(x => x.FirstName).Description("The first name of the consumer");
        Field(x => x.LastName).Description("The last name of the consumer");
        Field(x => x.Id);
        Field(x => x.Location, nullable: true); 

    }
}