using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MsLogLevel = Microsoft.Extensions.Logging.LogLevel;


namespace socketServer.Models
{
    public class appSettings
    {
        public Config Config { get; set; }
        public Connectionstrings ConnectionStrings { get; set; }
        public Logging Logging { get; set; }
       
    }
    public class Logging
    {
        public bool IncludeScopes { get; set; }
        public Loglevel LogLevel { get; set; }
        public string LogIMEI { get; set; }

    }
    public class Loglevel
    {
        public MsLogLevel Default { get; set; }
        public MsLogLevel System { get; set; }
        public MsLogLevel Microsoft { get; set; }
    }
    public class Config
    {
        public string Ip { get; set; } = "127.0.0.1";
        public string Port { get; set; } = "6060";       
        public int BufferSize { get; set; }
      
    }
    public class Connectionstrings
    {
        public string DefaultConnection { get; set; }
    }
}
