using Microsoft.Extensions.Logging;
using MsLogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace WebApplication5.Model
{
    public class Settings
    {
        public CompanyInfo companyInfo { get; set; }
        public Connectionstrings ConnectionStrings { get; set; }
        public required Logging Logging { get; set; }
    }
    public class Connectionstrings
    {
        public string? DefaultConnection { get; set; }
    }
    public class CompanyInfo
    {
        public string name { get; set; }
        public string version { get; set; }
        public string filePath { get; set; }
    }
    public class Logging
    {
        public bool IncludeScopes { get; set; }
        public Loglevel? LogLevel { get; set; }

    }
    public class Loglevel
    {
        public MsLogLevel Default { get; set; }
        public MsLogLevel System { get; set; }
        public MsLogLevel Microsoft { get; set; }
    }
}
