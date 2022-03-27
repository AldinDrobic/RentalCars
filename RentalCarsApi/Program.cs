using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RentalCarsApi.Data;
using System.Reflection;
using System.Text.Json.Serialization;
using RentalCarsApi.Services;
using RentalCarsApi.Services.CarServices;
using RentalCarsApi.Services.ReservationServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<RentalCarsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped(typeof(ICarService), typeof(CarService));
builder.Services.AddScoped(typeof(IReservationService), typeof(ReservationService));
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Rental Cars API",
        Version = "v1",
        Description = "An ASP.NET core Web API for managing car renting data.",
        Contact = new OpenApiContact
        {
            Name = "Rental Cars contact",
            Url = new Uri("https://github.com/AldinDrobic/RentalCars"),
        },
        License = new OpenApiLicense
        {
            Name = "Use under MIT",
            Url = new Uri("https://opensource.org/licenses/MIT"),
        }
    });
    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
