
using System.Reflection;
using WebApplication5.Controllers;
using WebApplication5.Model;
using WebApplication5.Services;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);
/*read section of config file for generat wwwroot folder dynamic*/
var config = new ConfigurationBuilder()
             .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
             .AddJsonFile("appsettings.json", optional: false)
             .Build();
var savefolderFile = config.GetValue<string>("CompanyInfo:filePath");

var path1 = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar+ savefolderFile; 
if (!Directory.Exists(path1)) //created in bin folder
{
    Directory.CreateDirectory(path1);
}
var path2 = Directory.GetParent(Environment.CurrentDirectory).GetDirectories()[1].FullName + Path.DirectorySeparatorChar + savefolderFile;
if (!Directory.Exists(path2)) //created in solution 
{
    Directory.CreateDirectory(path2);
}



// Add services to the container.
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
// Add services to the container.

builder.Services.Configure<Settings>(builder.Configuration.GetSection("Settings"));

builder.Services.AddSignalR();
builder.Services.AddSingleton<SocketService>();
//builder.Services.AddHostedService<SocketBackgroundService>();

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


app.UseCors("AllowAllOrigins"); 
app.MapHub<DataHub>("/dataHub");
app.MapControllers();
app.Run();
