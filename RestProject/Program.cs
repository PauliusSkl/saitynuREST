

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


using Microsoft.IdentityModel.Tokens;
using System.Text;
using RestProject.Auth;
using RestProject.Auth.Model;
using RestProject.Data;
using RestProject.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Npgsql.EntityFrameworkCore.PostgreSQL;

var builder = WebApplication.CreateBuilder(args);
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

//Microsoft.EntityFrameworkCore.SqlServer
//Microsoft.EntityFrameworkCore.Tools
// dotnet tool install --global dotnet-ef
//Microsoft.AspNetCore.Identity
//Microsoft.AspNetCore.Identity.EntityFrameworkCore
//Microsoft.AspNetCore.Authentication.JwtBearer

builder.Services.AddControllers();



builder.Services.AddIdentity<ForumRestUser, IdentityRole>()
        .AddEntityFrameworkStores<ForumDbContext>()
        .AddDefaultTokenProviders();


builder.Services.AddDbContext<ForumDbContext>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters.ValidAudience = builder.Configuration["JWT:ValidAudience"];
        options.TokenValidationParameters.ValidIssuer = builder.Configuration["JWT:ValidIssuer"];
        options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]));
    });

builder.Services.AddTransient<ITopicsRepository, TopicsRepository>();
builder.Services.AddTransient<IPostsRepository, PostsRepository>();
builder.Services.AddTransient<ICommentsRepository, CommentsRepository>();
builder.Services.AddTransient<IJwtTokenService, JwtTokenService>();
builder.Services.AddSingleton<IAuthorizationHandler, ResourceOwnerAuthorizationHandler>();

builder.Services.AddScoped<AuthDbSeeder>();


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(PolicyNames.ResourceOwner, policy => policy.Requirements.Add(new ResourceOwnerRequirement()));
});


var app = builder.Build();

app.UseRouting();

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<ForumDbContext>();
dbContext.Database.Migrate();

var dbSeeder = app.Services.CreateScope().ServiceProvider.GetRequiredService<AuthDbSeeder>();
await dbSeeder.SeedAsync();

app.Run();
