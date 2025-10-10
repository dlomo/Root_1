using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

// Create builder
var builder = WebApplication.CreateBuilder(args);

// =======================
// Configure Services
// =======================

// Add controllers
builder.Services.AddControllers();

// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Entity Framework Core (replace with your actual DB context & connection string)
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
 //   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add IdentityCore if using ASP.NET Core Identity
//builder.Services.AddIdentityCore<IdentityUser>()
 //   .AddEntityFrameworkStores<ApplicationDbContext>();

// Add JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"] ?? "SurgeryApp",
            ValidAudience = builder.Configuration["Jwt:Audience"] ?? "SurgeryAppUsers",
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? "SuperSecretKey12345"))
        };
    });

// Add Authorization
builder.Services.AddAuthorization();

var app = builder.Build();

// =======================
// Configure Middleware
// =======================

// Enable Swagger in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SurgeryApp API V1");
        c.RoutePrefix = string.Empty; // Serve Swagger UI at app root
    });
}

app.UseHttpsRedirection();

// JWT Authentication must come BEFORE Authorization
app.UseAuthentication();
app.UseAuthorization();

// Map controller endpoints
app.MapControllers();

app.Run();
