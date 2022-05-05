using GraphQL.Types;

namespace GraphQLAPI.Entities;

public class VirtualBookType : ObjectGraphType<VirtualBook> {
    public VirtualBookType() {
        Name = "virtuaBook";

        Description = "Represents the virtual book";

        // This is how you expose a field with a description
        Field(x => x.Title).Description("The title of the book");

        // Expose nullable field
        Field(x => x.Description, nullable: true).Description("Short description describing the entire book and author");
        Field(x => x.IsEntryLevel).Description("True if book is an for entry level reader");

        // And that, my friends, is how you do relationships
        // Note, that this will only expose the field, it will not mean that the items will be returned automatically
        // for that, we need to update our service to include the child element. See MyAwesomeService.cs
        Field<ListGraphType<ConsumerType>>("purchasedBy", "The list of consumer who have purchased the book");

    }
}