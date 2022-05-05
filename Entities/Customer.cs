namespace GraphQLAPI.Entities;

public class Customer {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Guid Id { get; set; }
    public string Location { get; set; }
}