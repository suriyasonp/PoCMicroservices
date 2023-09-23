using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Poc.Services.CouponAPI;
using Poc.Services.CouponAPI.Data;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var provider = configuration.GetValue("Provider", "SqlServer");

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(
options => _ = provider switch
{
    "Sqlite" => options.UseSqlite(
        configuration.GetConnectionString("SqliteConnection"),
       x => x.MigrationsAssembly("Poc.Services.CouponAPI")
       ),

    "SqlServer" => options.UseSqlServer(
        configuration.GetConnectionString("SqlServerConnection"),
        x => x.MigrationsAssembly("Poc.Services.CouponAPI")),

    _ => throw new Exception($"Unsupported provider: {provider}")
});

// Add Auto Mapper Service
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

ApplyMigration();

app.Run();

void ApplyMigration()
{
    try
    {
        using var scope = app.Services.CreateScope();
        var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var x = _db.Database.GetPendingMigrations().ToString();

        if (_db.Database.GetPendingMigrations().Count() > 0)
        {
            _db.Database.Migrate();
        }
    }
    catch (System.Exception ex)
    {
        throw ex;
    }
}
