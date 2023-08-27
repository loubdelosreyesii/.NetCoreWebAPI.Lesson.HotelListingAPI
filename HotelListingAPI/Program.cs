using HotelListingAPI.Configurations;
using HotelListingAPI.Contracts;
using HotelListingAPI.Data;
using HotelListingAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Lesson 3.1: Get the connection string from your appsettings.json file.
var connectionString = builder.Configuration.GetConnectionString("HotelListingDbConnectionString");
//Lesson 3.2
builder.Services.AddDbContext<HotelListingDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddControllers();  
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(
    options => {
        options.AddPolicy("AllowAll",
            b => b.AllowAnyHeader()
                .AllowAnyOrigin()
                .AllowAnyMethod());
});

//Lesson 2 : Add Serilog 
builder.Host.UseSerilog((ctx,lc)=>lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

//Adding AutoMapper
builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ICountriesRepository,CountriesRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
