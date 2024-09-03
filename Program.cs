

using GoldenS.DatabaseContext;
using GoldenS.Graphql.Query;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer("Server=DESKTOP-GMI2BNJ; Database=GoldenS; Integrated Security=True;"));

builder.Services
    .AddScoped<AppDbContext>()
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddProjections()
    .AddFiltering()
    .AddSorting();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();
app.MapGraphQL("/graphql");

// app.UseEndpoints(endpoints =>
// {
//     endpoints.MapControllers();
//     endpoints.MapGraphQL("/graphql");
// });

// app.UseGraphQLPlayground(new PlaygroundOptions
// {
//     Path = "/playground"
// });

app.Run();
