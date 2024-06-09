
using WebApplication5.Model;

var builder = WebApplication.CreateBuilder(args);
//var config = new ConfigurationBuilder()
//             .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
//             .AddJsonFile("appsettings.json", optional: false)
//             .Build();
//var name = config.GetValue<string>("CompanyInfo:name");

// Add services to the container.
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.Configure<Settings>(Conf => builder.Configuration.Bind(Conf));

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins"); 
app.UseAuthorization();

app.MapControllers();

app.Run();
