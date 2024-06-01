using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using socketServer.Classes;
using socketServer.Interface;



//var basePath = Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName;

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);


IConfigurationRoot configuration = builder.Build();
Console.WriteLine(configuration["Config:Ip"]); // Outputs: 127.0.0.1
Console.WriteLine(configuration["Config:Port"]); // Outputs: 6060
Console.WriteLine(configuration["Config:BufferSize"]); // Outputs: 102

var serviceCollection = new ServiceCollection();
serviceCollection.AddSingleton<IConfiguration>(configuration);
serviceCollection.AddSingleton<IActions,Actions>();
serviceCollection.AddSingleton<ISocketListener, SocketListener>();

// Add other services here
IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

var startPrg = serviceProvider.GetService<ISocketListener>();
startPrg.StartListening();
