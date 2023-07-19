using LibraryMgmt.EFCore.DataAccess.LibDbContext;
using LibraryMgmt.EFCore.DataAccess.Repositories;
using LibraryMgmt.EFCore.DataAccess.Services;
using LibraryMgmt.EFCore.DataAccess.UnitOfWork;
using LibraryMgmt.EFCore.Domain.Interfaces;
using LibraryMgmt.EFCore.Domain.Repository;
using LibraryMgmt.WebAPI.Middlewares;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using LibraryMgmt.EFCore.Service.Services.Interfaces;
using LibraryMgmt.EFCore.Service.Services;
using LibraryMgmt.EFCore.DataAccess;
using LibraryMgmt.WebAPI.JWTAuthenticationAuthorization.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    // Configure Swagger to use JWT authentication
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header, 
        Name = "Authorization" 
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
        new string[] { }
    }
});
});


builder.Services.AddDbContext<LibraryDbContext>(
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("LibraryDBConnection"))
    .EnableSensitiveDataLogging()
    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

//Configuring JWT authentication
builder.Services.AddScoped<IUserLoginRepository, UserLoginRepository>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = SecurityKeyGenerator.GetSymmetricSecurityKey(builder.Configuration)
    };
});

#region Repositories
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient<IStaffRepository, StaffRepository>();
builder.Services.AddTransient<IBookRepository, BookRepository>();
builder.Services.AddTransient<ICustomerRepository,CustomerRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
#endregion Repositories


#region Services
builder.Services.AddScoped<IBookCrudService, BookCrudService>();
builder.Services.AddScoped<IStaffCrudService, StaffCrudService>();
builder.Services.AddScoped<ICustomerCrudService, CustomerCrudService>();
builder.Services.AddScoped<IOrderCrudService, OrderCrudService>();
builder.Services.AddScoped<ICreateOrderService, CreateOrderService>();
builder.Services.AddScoped<IUserLoginJWTokenService, UserLoginJWTokenService>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

#endregion Services
builder.Services.AddLogging();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseMiddleware<LoggingMiddleware>();

app.Run();

