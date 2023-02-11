using _.WebAPI.Core.IConfiguration;
using _.WebAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ModelContext>(options => options
    .UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty)
    .EnableSensitiveDataLogging()
    .LogTo(Console.WriteLine, LogLevel.Information)
    .EnableDetailedErrors());
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddCors(options =>
{
    options.AddPolicy( "CorsDevelopmentPolicy",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                // Remember to change these during actual production for security reasons
                .AllowAnyHeader() // Alt is .WithHeaders("Content-Type", "Accept", "Authorization")us
                .AllowAnyMethod(); // Alt is.WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS")
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("CorsDevelopmentPolicy");

app.Run();
