using Microsoft.Extensions.Options;
using System.IO;
using System.Threading.Tasks;
using WebApplication5.Model;
namespace WebApplication5.Services
{
    public class FileService
    {
        private readonly Settings _settings;

        public FileService(IOptions<Settings> settings)
        {
            _settings = settings.Value;
        }

        public async Task ReceiveDataToFileAsync(byte[] data)
        {
            string fileName = $"{DateTime.Now:yyyyMMdd_HHmmss}.Receive.bin";

            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _settings.companyInfo.filePath, fileName);

            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            await File.WriteAllBytesAsync(filePath, data);
        }

        public  async Task SendDataToFileAsync(string message, bool append = false)
        {
            var filename = DateTime.Now.ToString("yyyy-dd-M-HH-mm-ss") + ".Send.bin";
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _settings.companyInfo.filePath, filename);
            try
            {
                using (FileStream stream = new FileStream(Path.Combine(filePath, filename), append ? FileMode.Append : FileMode.Create, FileAccess.Write, FileShare.None, 4096, true))
                using (StreamWriter sw = new StreamWriter(stream))
                {
                    await sw.WriteAsync(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to file: {ex.Message}");
                // Handle the exception (e.g., log it or take corrective action)
            }
        }
    }

}