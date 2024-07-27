using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Microsoft.Extensions.Configuration;


[assembly: ArmDot.Client.VirtualizeCode]

class Program
{
    static async Task Main(string[] args)
    {
        var config = LoadConfiguration();
        var serverSettings = new ServerSettings();
        config.GetSection("ServerSettings").Bind(serverSettings);
        StartHttpServer(serverSettings);
        var listener = new TcpListener(IPAddress.Any, serverSettings.Port);
        listener.Start();
        Console.WriteLine($"Listening on port {serverSettings.Port}...");

        while (true)
        {
            var client = await listener.AcceptTcpClientAsync();
            _ = HandleClientAsync(client, serverSettings);
        }
    }

    static IConfiguration LoadConfiguration()
    {
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
    }

    static async Task HandleClientAsync(TcpClient client, ServerSettings settings)
    {
        Console.WriteLine($"Client connected {DateTime.Now}.");

        var outputDirectory = settings.OutputDirectory;
        Directory.CreateDirectory(outputDirectory);

        int fileIndex = 0;
        DateTime startTime = DateTime.Now;
        string outputFile = GetOutputFileName(outputDirectory, fileIndex, settings.FileExtension);

        using (var networkStream = client.GetStream())
        {
            var buffer = new byte[4096];
            int bytesRead;

            while ((bytesRead = await networkStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                // Write to file
                using (var fileStream = new FileStream(outputFile, FileMode.Append, FileAccess.Write, FileShare.None))
                {
                    await fileStream.WriteAsync(buffer, 0, bytesRead);
                }

                // Check save interval
                var currentTime = DateTime.Now;
                if ((currentTime - startTime).TotalMinutes >= settings.SaveIntervalMinutes)
                {
                    fileIndex++;
                    outputFile = GetOutputFileName(outputDirectory, fileIndex, settings.FileExtension);
                    startTime = currentTime;
                }
            }
        }

        client.Close();
    }
    static string GetOutputFileName(string outputDirectory, int index, string fileExtension)
    {
        return Path.Combine(outputDirectory, $"video_{index}.{fileExtension}");
    }


    static void StartHttpServer(ServerSettings settings)
    {
        var outputDirectory = settings.OutputDirectory;
        var listener = new HttpListener();
        listener.Prefixes.Add($"http://localhost:{settings.HttpPort}/");
        listener.Start();
       // Console.WriteLine($"HTTP server running on port {settings.HttpPort}...");

        Task.Run(async () =>
        {
            while (true)
            {
                var context = await listener.GetContextAsync();
                var request = context.Request;
                var response = context.Response;

                var filePath = request.Url.LocalPath.TrimStart('/');
                var fullPath = Path.Combine(outputDirectory, filePath);

                if (File.Exists(fullPath))
                {
                    try
                    {
                        using (var fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            await fileStream.CopyToAsync(response.OutputStream);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error serving file '{filePath}': {ex.Message}");
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    }
                }
                else
                {
                    Console.WriteLine($"File '{filePath}' not found.");
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                }

                response.Close();
            }
        });
    }

}

class ServerSettings
{
    public int Port { get; set; }
    public int HttpPort { get; set; }
    public string OutputDirectory { get; set; }
    public string FileExtension { get; set; }
    public int SaveIntervalMinutes { get; set; }
}
