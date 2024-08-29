using SalesDataPredictionAPI.Data;
using SalesDataPredictionAPI.Repositories;
using SalesDataPredictionAPI.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add the factory to the dependency injection container
builder.Services.AddSingleton<DbConnectionFactory>();
// Add the repository to the dependency injection container
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IOrderClientRepository, OrderClientRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IShipperRepository, ShipperRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Add Cors with allowed connections
var allowedOrigins = builder.Configuration.GetSection("AllowedOriginsApp").Get<string[]>();

if (allowedOrigins == null || allowedOrigins.Length == 0)
{
    throw new ArgumentNullException(nameof(allowedOrigins), "The allowed origin is not configured in appsettings.json.");
}

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowedOrigins",
    policy =>
    {
        policy.WithOrigins(allowedOrigins)
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowedOrigins");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
