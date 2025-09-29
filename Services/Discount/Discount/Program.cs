using Discount.Extensions;
using Discount.Handlers;
using Discount.Repositories;
using Discount.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Mediatr
var asemblies = new Assembly[]
{
    Assembly.GetExecutingAssembly(), typeof(CreateDiscountHandler).Assembly
};
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(asemblies));
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
builder.Services.AddGrpc();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

//Migrate Database
app.MigrateDatabase<Program>();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<DiscountService>();
});

app.Run();
