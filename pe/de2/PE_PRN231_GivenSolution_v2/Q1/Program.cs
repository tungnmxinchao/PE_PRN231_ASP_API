using AutoMapper;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Q1;
using Q1.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PE_PRN231_Sum23B5Context>(otp =>
{
    otp.UseSqlServer(builder.Configuration.GetConnectionString("MyCnn"));
});

var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new MapperConfig()));
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddControllers();

//    .AddOData(opt =>
//    {
//        opt.Select()
//                .Filter()
//                .OrderBy()
//                .Expand()
//                .Count()
//                .SetMaxTop(100)
//                .AddRouteComponents("api", GetEdmModel());
//    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();

//static IEdmModel GetEdmModel()
//{
//    var builder = new ODataConventionModelBuilder();

//    var booksEntity = builder.EntitySet<BookResponse>("books").EntityType;
//    booksEntity.HasKey(k => k.BookId);

//    return builder.GetEdmModel();
//}