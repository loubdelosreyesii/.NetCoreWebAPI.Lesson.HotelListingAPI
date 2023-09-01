using HotelListing.API.Core.Configurations;
using HotelListing.API.Core.Contracts;
using HotelListing.API.Data;
using HotelListing.API.Core.Middleware;
using HotelListing.API.Core.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Lesson 3.1: Get the connection string from your appsettings.json file.
var connectionString = builder.Configuration.GetConnectionString("HotelListingDbConnectionString");
//Lesson 3.2
builder.Services.AddDbContext<HotelListingDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

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
builder.Services.AddScoped<IHotelsRepository,HotelsRepository>();
builder.Services.AddScoped<IAuthManager, AuthManager>();
//Adding IdentityCore
//1. Add Nuget Package : Microsoft.AspNetCore.Identity.FrameworkCore
//2. Add the line of code below
//3. Use IdentityUser or customer UserClass in the AddIdentityCore<>
builder.Services.AddIdentityCore<ApiUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<ApiUser>>("HotelListing.API.Core")
    .AddEntityFrameworkStores<HotelListingDbContext>()
    .AddDefaultTokenProviders();
//
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
            new QueryStringApiVersionReader("api-version"),
            new HeaderApiVersionReader("X-Version"),
            new MediaTypeApiVersionReader("ver")
        );
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options=>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
        };
    }
);
//Add Response Caching
builder.Services.AddResponseCaching(options => 
{
    options.MaximumBodySize = 1024; //largest cacheable to be allowed.
    options.UseCaseSensitivePaths = true; //if somebody request
});

builder.Services.AddControllers().AddOData(options => 
{
    options.Select().Filter().OrderBy();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

//this 2 lines of codes should be declare after adding the Cors.
app.UseResponseCaching(); //enabling the middleware

//creating the middleware instructions
app.Use(async(context, next)=>
{
    //adding values to the cache headers
    context.Response.GetTypedHeaders().CacheControl = new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
    {
        Public = true,
        MaxAge = TimeSpan.FromSeconds(10),
    };

    //use to vary the cache response. 
    context.Response.Headers[HeaderNames.Vary] = new string[] { "Accept-Encoding" };
    
    await next();
});

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
