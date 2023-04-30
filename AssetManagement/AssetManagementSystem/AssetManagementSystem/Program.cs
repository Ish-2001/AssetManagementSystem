using AssetManagementSystem;
using AssetManagementSystem.DAL;
using AssetManagementSystem.Data.Models;
using AssetManagementSystem.Logging;
using AssetManagementSystem.Models;
using AssetManagementSystem.Services;
using AssetManagementSystem.Services.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSingleton<ILog, NLogging>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<DBContext>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["JwtConfiguration:Issuer"],
        ValidAudience = builder.Configuration["JwtConfiguration:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfiguration:TokenSecret"]))
        //ClockSkew = TimeSpan.Zero
    };
});


//builder.Services.AddScoped<IAssetAssignmentService, AssetAssignmentService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IAssetDetailsService, AssetDetailsService>();
builder.Services.AddScoped<IAssetAssignmentService, AssetAssignmentService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IJwtService, JwtService>();  

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
