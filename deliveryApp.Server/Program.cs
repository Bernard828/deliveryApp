using deliveryApp.Server;
using deliveryApp.Server.CompiledModels;
using deliveryApp.Server.Data;
using deliveryApp.Server.Endpoints;
using deliveryApp.Server.NewFolder;
using deliveryApp.Server.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

var builder = WebApplication.CreateSlimBuilder(args);
//var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DeliveryAppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
.UseModel(DeliveryAppDbContextModel.Instance)
);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    // Match your frontend preferences perfectly:
    // For camelCase properties (name, cuisineTypeId) use JsonNamingPolicy.CamelCase
    // For PascalCase properties (Name, CuisineTypeId) set it to null
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

    // Wire up your safe AOT compiler registry
    options.SerializerOptions.TypeInfoResolver = AppJsonContext.Default;

});

builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddScoped<ICuisineTypeService, CuisineTypeService>();
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<IMenuItemService, MenuItemService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IOrderService, OrderService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("DeliveryAppExchange", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
       .AllowAnyHeader()
       .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//app.MapGet("/api/MenuItem", () => Results.Ok(new { Status = "Healthy" }));
//app.MapGet("/api/Orders", () => Results.Ok(new { Status = "Healthy" }));
//app.MapRestaurantEndpoints();
//app.MapGet("/api/User", () => Results.Ok(new { Status = "Healthy" }));

// 1. A Simple Inline Endpoint
//app.MapGet("/api/status", () => Results.Ok(new { Status = "Healthy" }));
//
// 2. An Endpoint with Dependency Injection and Parameters
//app.MapGet("/api/orders/{id}", (int id, MyService service) =>
//{
//    var order = service.GetOrder(id);
//    return order is not null ? Results.Ok(order) : Results.NotFound();
//});
app.MapControllers();
app.UseCors("DeliveryAppExchange");

app.MapFallbackToFile("/index.html");

app.Run();
