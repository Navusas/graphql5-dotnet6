namespace GraphQLAPI.Entities
{
    public class VirtualBook
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PageCount { get; set; }
        public bool IsEntryLevel { get; set; }
        public List<Customer> PurchasedBy { get; set; }
    }
}
