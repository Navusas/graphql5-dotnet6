using Bogus;
using GraphQL;
using GraphQL.AspNetCore3;
using GraphQL.MicrosoftDI;
using GraphQL.Server.Ui.Playground;
using GraphQL.SystemTextJson;
using GraphQL.Types;
using GraphQLAPI;
using GraphQLAPI.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("Helloworld")
);
builder.Services.AddTransient<IMyAwesomeService, MyAwesomeService>();

builder.Services.AddSingleton<ISchema, MyApplicationSchema>(services => new MyApplicationSchema(new SelfActivatingServiceProvider(services)));

builder.Services.AddGraphQL(b => b
    .AddGraphTypes(typeof(Program).Assembly)
    .AddSchema<MyApplicationSchema>()
    .AddSystemTextJson()   // serializer
);

var app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseWebSockets();
app.UseGraphQL("/graphql");  // url to host GraphQL endpoint

//app.UseGraphQLGraphiQL();

app.UseGraphQLPlayground(
    new PlaygroundOptions {
        GraphQLEndPoint = new PathString("/graphql"),
        SubscriptionsEndPoint = new PathString("/graphql"),
    },
    "/");   // url to host Playground at


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();

    var faker = new Faker<Customer>()
        .RuleFor(r => r.FirstName, f => f.Name.FirstName())
        .RuleFor(r => r.LastName, f => f.Name.LastName())
        .RuleFor(r => r.Id, f => Guid.NewGuid())
        .RuleFor(r => r.Location, f => f.Address.Country());


    var bookFaker = new Faker<VirtualBook>()
        .RuleFor(r => r.Id, f => Guid.NewGuid())
        .RuleFor(x => x.Title, f => f.Lorem.Sentence())
        .RuleFor(x => x.Description, f => f.Lorem.Text())
        .RuleFor(x => x.PurchasedBy, f => faker.Generate(new Random().Next(0, 10)));

    var fakeData = bookFaker.Generate(25);
    context.VirtualBooks.AddRange(fakeData);
    context.SaveChanges();
}

await app.RunAsync();