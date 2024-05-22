using Microsoft.OpenApi.Models;
using Serilog;
using SponsorSphere.Application;
using SponsorSphere.Domain.Models;
using SponsorSphere.Infrastructure;
using SponsorSphere.Infrastructure.Extensions;
using SponsorSphereWebAPI.Extensions;
using SponsorSphereWebAPI.Filters;
using Swashbuckle.AspNetCore.Filters;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => options.Filters.Add<DurationLogActionFilter>())
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
}
);

builder.Services.AddIdentityApiEndpoints<User>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Lockout.MaxFailedAccessAttempts = 5;
})
.AddRoles<UserRole>()
.AddEntityFrameworkStores<SponsorSphereDbContext>();

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

builder.Services.AddInfrastructure();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AssemblyMarker).Assembly));
builder.Host.UseSerilog((hostingContext, loggerConfiguration) => { loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration); });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapIdentityApi<User>();
app.UseExceptionHandling();

// Debug CORS headers
app.Use(async (context, next) =>
{
    await next.Invoke();
    if (context.Response.Headers.TryGetValue("Access-Control-Allow-Origin", out Microsoft.Extensions.Primitives.StringValues value))
    {
        var corsHeaders = value;
        Console.WriteLine("CORS headers: " + corsHeaders);
    }
    else
    {
        Console.WriteLine("CORS headers are not set.");
    }
});
app.UseCors("AllowSpecificOrigins");
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
