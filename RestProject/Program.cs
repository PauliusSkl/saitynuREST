using RestProject.Data;
using RestProject.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

//Microsoft.EntityFrameworkCore.SqlServer
//Microsoft.EntityFrameworkCore.Tools
// dotnet tool install --global dotnet-ef


builder.Services.AddControllers();

builder.Services.AddDbContext<ForumDbContext>();
builder.Services.AddTransient<ITopicsRepository, TopicsRepository>();
builder.Services.AddTransient<IPostsRepository, PostsRepository>();
builder.Services.AddTransient<ICommentsRepository, CommentsRepository>();




var app = builder.Build();

app.UseRouting();

app.MapControllers();



app.Run();
