using ArtisanShopAPI.Data;
using ArtisanShopAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add Database Context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Azure Blob Storage
builder.Services.AddScoped<IBlobStorageService, BlobStorageService>();

// Add Email Service
builder.Services.AddScoped<IEmailService, EmailService>();

// Add Formatting Service
// P.S. Honest to god i don't know if should actually make the formatting service
// an interface instead of just a concrete class with no interface.
builder.Services.AddScoped<IFormattingService, FormattingService>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

// Add Angular to CORS rules
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("AllowAngular");
app.MapControllers();
app.Run();
