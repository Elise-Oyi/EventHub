using EventHub.Dal;
using EventHub.Jwt;
using EventHub.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//--adding data context in service container
builder.Services.AddDbContext<EventHubDbContext>(options =>
  options.UseNpgsql(builder.Configuration.GetConnectionString("conn"))
);

//--adding common repository as a service
builder.Services.AddTransient<ICommonRepository<Employee>, CommonRepository<Employee>>();
builder.Services.AddTransient<ICommonRepository<Event>, CommonRepository<Event>>();
//--adding Authentication repository as a service(non-generic repository)
builder.Services.AddTransient<IAuthenticationRepository, AuthenticationRepository>();
//--adding Jwt service
builder.Services.AddScoped<ITokenManager, TokenManager>();
//--adding automapper service
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//--adding CORS as a service
builder.Services.AddCors(options =>
{
    options.AddPolicy("PublicPolicy", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();    
        policy.AllowAnyOrigin();
    });
});

//--adding authentication middleware
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:secret"])),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
      options =>
      {
          options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
          {
              In = ParameterLocation.Header,
              Description = "Authentication Token",
              Name = "Authorization",
              Type = SecuritySchemeType.Http,
              BearerFormat = "JsonWebToken",
              Scheme = "Bearer"
          });
          options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{} 
        }
    });
      }
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//--CORS middleware
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
