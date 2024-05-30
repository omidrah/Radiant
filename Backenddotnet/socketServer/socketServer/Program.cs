using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using socketServer.Classes;
using socketServer.Interface;
using socketServer.Models;

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);


IConfigurationRoot configuration = builder.Build();
var appSettings = configuration.Get<appSettings>();


var serviceCollection = new ServiceCollection();
serviceCollection.AddSingleton(appSettings);
serviceCollection.AddSingleton<IActions,Actions>();
// Add other services here
IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

