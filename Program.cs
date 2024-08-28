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
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowedOrigins",
    policy =>
    {
        policy.WithOrigins("AllowedOriginsApp")
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
