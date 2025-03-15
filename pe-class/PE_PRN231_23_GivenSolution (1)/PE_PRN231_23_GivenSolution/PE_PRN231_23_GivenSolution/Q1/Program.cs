using Microsoft.AspNetCore.OData;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Q1.Models;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Q1.DTO;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddOData(opt => opt
                .Select()
                .Filter()
                .OrderBy()
                .Expand()
                .Count()
                .SetMaxTop(100)
                .AddRouteComponents("odata", GetEdmModel())
            );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PE_PRN_Fall2023B1Context>(
    option => option.UseSqlServer(builder.Configuration.GetConnectionString("MyCnn")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

 static IEdmModel GetEdmModel()
{
    var builder = new ODataConventionModelBuilder();

    builder.EntitySet<MovieResponse>("Movie");
    return builder.GetEdmModel();
}
