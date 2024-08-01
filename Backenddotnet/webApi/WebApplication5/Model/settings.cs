using Microsoft.Extensions.Logging;
using MsLogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace WebApplication5.Model
{
    public class Settings
    {
        public CompanyInfo companyInfo { get; set; }
        public SocketConfig SocketConfig { get; set; }

        public Connectionstrings ConnectionStrings { get; set; }
        public required Logging Logging { get; set; }
    }
    public class Connectionstrings
    {
        public string DefaultConnection { get; set; }  = null!;

    }
    public class CompanyInfo
    {
        public string name { get; set; }  = null!;
        public string version { get; set; } = null!;
        public string filePath { get; set; } = null!;

    }
    public class SocketConfig
    {
        public string IpAddress { get; set; }
        public int Port { get; set; }
    }
    public class Logging
    {
        public bool IncludeScopes { get; set; }
        public Loglevel LogLevel { get; set; }

    }
    public class Loglevel
    {
        public MsLogLevel Default { get; set; }
        public MsLogLevel System { get; set; }
        public MsLogLevel Microsoft { get; set; }
    }
}
