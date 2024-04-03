using OblakotekaServer.DataAccess;
using OblakotekaServer.Domain;
using OblakotekaServer.Domain.Mappers;
using OblakotekaServer.Middlewares;
using OblakotekaServer.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ServiceConfiguration>(
    builder.Configuration
        .SetBasePath(builder.Environment.ContentRootPath)
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables()
        .Build()
);
builder.Services.AddAutoMapper(typeof(ProductMapProfile));
builder.Services.AddAutoMapper(typeof(ProductEditProfile));
builder.Services.AddAutoMapper(typeof(ProductCreateProfile));

builder.Services.AddDbContext<TestDbContext>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped(typeof(CancellationToken), x =>
x.GetRequiredService<IHttpContextAccessor>().HttpContext?.RequestAborted ?? CancellationToken.None
);
builder.Services.AddScoped<ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<DomainExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
