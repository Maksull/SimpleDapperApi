using DapperApi.Data.Db;
using DapperApi.DI;
using DapperApi.Endpoints;
using DapperApi.Migrations.Dapper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region Services

builder.Services.AddControllers();

builder.ConfigureDI();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddEntityFrameworkNpgsql().AddDbContext<ApiDataContext>(opts =>
{
    opts.UseNpgsql(builder.Configuration.GetConnectionString("DapperTestApiPostgres"));
});

builder.Services.AddSingleton<DapperContext>();
builder.Services.AddSingleton<Database>();

//builder.Services.AddFluentMigratorCore().ConfigureRunner(c => c.AddPostgres()
//    .WithGlobalConnectionString(builder.Configuration.GetConnectionString("SlaveConnectionPostgres"))
//    .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations()
//);

#endregion

var app = builder.Build();

#region App

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapProductsEndpoints();
app.MapCategoriesEndpoints();

app.MapControllers();

//app.MigrateDatabase();

app.EnsurePopulated();

#endregion

app.Run();
