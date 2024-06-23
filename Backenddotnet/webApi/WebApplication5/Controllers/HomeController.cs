using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Text.Json.Nodes;
using WebApplication5.Model;

namespace WebApplication5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly Settings _settings;

        public HomeController(ILogger<HomeController> logger, IOptions<Settings> settings)
        {
            _logger = logger;
            _settings = settings.Value;
        }
        [HttpGet]
        public string Get()
        {
            return _settings.companyInfo.name; 
        }
        /// <summary>
        /// آرایه ای از بایت ها دریافت شده  و باینری آن در فایل چاپ میشود
        /// </summary>
        /// <param name="inputString"></param>
        private async Task SavebyteArrayToFile(string  hexValue,AskResult valueFromUi)
         {
            //var cnt =  res.toByteArray().Length;    
            Console.WriteLine($"{DateTime.Now}");
            Console.WriteLine("*******************************************************************");
            Console.WriteLine($"Value in UI  {valueFromUi}");
            Console.WriteLine($"Value in Hex {hexValue}");
            Console.WriteLine("*******************************************************************");
            // Convert the hexadecimal string to a byte array
            byte[] byteArray = Utils.StringToByteArray(hexValue);
            //Console.WriteLine("Byte Array:");
            //foreach (byte b in byteArray)
            //{
            //    Console.Write(b.ToString("X2") + " ");
            //}
            configSocket(byteArray);
            // Start async Task to Save Image
            var pa= Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar + _settings.companyInfo.filePath;
            await Utils.FileWriteAsync(pa, valueFromUi.ToString() + "\n"+ hexValue);
        }
       
        [HttpPost("saveData")]
        public async Task<IActionResult> saveData([FromBody] JsonObject inputString)
        {
            AskResult res = new (); StringBuilder sb = new(); 
            foreach (var property in inputString)
            {
                string key = property.Key;
                switch (key)
                {
                    case "header":
                        if (!string.IsNullOrEmpty(property.Value.ToString()))
                        {
                            string head = property.Value.ToString();
                            //byte[] header = Encoding.ASCII.GetBytes(head);
                            //Array.Copy(header, 0, blockdata, 0, 3);
                            for (var i = 0; i < head.Length; i++)
                            {
                                res.header[i] = head[i];
                                sb.Append(Convert.ToByte(res.header[i]).ToString("X2"));
                            }
                        }
                        break;
                    case "testmode":

                        switch (property.Value.ToString())
                        {
                            case "txoff":
                                res.testmode = 1;
                                break;
                            case "directmds":
                                res.testmode = 2;
                                break;
                            case "directpwr":
                                res.testmode = 0;
                                break;
                        }
                        sb.Append(res.testmode.ToString("X2"));
                        break;                   
                    case "datamode":
                        switch (property.Value.ToString())
                        {
                            case "manual":
                                res.datamode = 1;
                                break;
                            case "simulate":
                                res.datamode = 2;
                             break;
                            case "loopback":
                                res.datamode = 0;
                                break;
                        }
                        sb.Append(res.datamode.ToString("X2"));
                        break;
                    case "att":
                        if (!string.IsNullOrEmpty(property.Value.ToString()))
                        {
                            _ = byte.TryParse(property.Value.ToString(), out byte att);
                            res.att = att;
                            sb.Append(res.att.ToString("X2"));
                        }
                        break;
                    case "mfreq":
                        if (!string.IsNullOrEmpty(property.Value.ToString()))
                        {
                            _ = byte.TryParse(property.Value.ToString(), out byte mfreq);
                            res.mfreq=  mfreq;//frequenty
                            sb.Append(res.mfreq.ToString("X2"));
                        }
                        break;
                    case "rsvd1":
                        byte rsvd1 = 0;
                            res.rsvd1 = rsvd1;
                            sb.Append(Utils.convertToHex(res.rsvd1));                        
                        break;

                    case "m1xm":
                        _ = int.TryParse(property.Value.ToString(), out int m1xm);
                        //byte[] M1_Xm = BitConverter.GetBytes(m1xm); //convert int to byteArray
                        //Array.Copy(M1_Xm, 0, blockdata, 7, 4);
                        res.m1xm = m1xm;
                        //string result = Utils.convertToHex(res.m1xm);
                        sb.Append(Utils.convertToHex(res.m1xm));
                        break;
                    case "m1ym":
                        _= int.TryParse(property.Value.ToString(), out int m1ym);
                        res.m1ym = m1ym;
                        sb.Append(Utils.convertToHex(res.m1ym));
                        break;
                    case "m1zm":
                        _ = int.TryParse(property.Value.ToString(), out int m1zm);
                       res.m1zm = m1zm;
                        sb.Append(Utils.convertToHex(res.m1zm));
                        break;
                    case "m1status":
                        _ = byte.TryParse(property.Value.ToString(), out byte m1staus);
                        res.m1status = m1staus;
                        sb.Append(Utils.convertToHex(res.m1status));
                        break;
                    case "m1adm":                        
                        byte m1adm =  (byte)Utils.ConvertBinaryStringToUInt32("00" + property.Value.ToString());                        
                        res.m1adm = m1adm;
                        sb.Append(Utils.convertToHex(res.m1adm));
                        break;
                    case "rsvd2":
                        byte[] rsvd2 = new byte[] { 0, 0 };
                        res.rsvd2 = rsvd2;
                        sb.Append(Utils.convertToHex(res.rsvd2));
                        break;
                   
                    case "m2xm":
                        _ = int.TryParse(property.Value.ToString(), out int m2xm);
                        res.m2xm = m2xm;
                        sb.Append(Utils.convertToHex(res.m2xm));
                        break;
                    case "m2ym":                      
                        _ = int.TryParse(property.Value.ToString(), out int m2ym);
                        res.m2ym = m2ym;
                        sb.Append(Utils.convertToHex(res.m2ym));
                        break;
                    case "m2zm":
                        _ = int.TryParse(property.Value.ToString(), out int m2zm);
                        res.m2zm= m2zm;
                        sb.Append(Utils.convertToHex(res.m2zm));
                        break;
                    case "m2status":
                        _ = byte.TryParse(property.Value.ToString(), out byte m2staus);
                        res.m2status = m2staus;
                        sb.Append(Utils.convertToHex(res.m2status)); 
                        break;
                    case "m2adm":
                        byte m2adm = (byte)Utils.ConvertBinaryStringToUInt32("00" + property.Value.ToString());
                        res.m2adm = m2adm;
                        sb.Append(Utils.convertToHex(res.m2adm)); 
                        break;
                    case "rsvd3":
                        byte[] rsvd3 =  { 0, 0 };
                        res.rsvd3 = rsvd3;
                        sb.Append(Utils.convertToHex(res.rsvd3));
                        break;

                    case "m3xm":
                        _ = int.TryParse(property.Value.ToString(), out int m3xm);
                        res.m3xm = m3xm;    
                        sb.Append(Utils.convertToHex(res.m3xm)); 
                        break;
                    case "m3ym":
                        _ = int.TryParse(property.Value.ToString(), out int m3ym);
                        res.m3ym = m3ym;    
                        sb.Append(Utils.convertToHex(res.m3ym));
                        break;
                    case "m3zm":
                        _ = int.TryParse(property.Value.ToString(), out int m3zm);
                        res.m3zm = m3zm;
                        sb.Append(Utils.convertToHex(res.m3zm));
                        break;
                    case "m3status":
                        _ = byte.TryParse(property.Value.ToString(), out byte m3staus);
                        res.m3status=  m3staus;    
                        sb.Append(res.m3status.ToString("X2"));
                        break;
                    case "m3adm":
                        byte m3adm = (byte)Utils.ConvertBinaryStringToUInt32("00" + property.Value.ToString());
                        res.m3adm = m3adm;  
                        sb.Append(Utils.convertToHex(res.m3adm));
                        break;
                    case "rsvd4":
                        byte[] rsvd4 = new byte[] { 0, 0 };
                        res.rsvd4 = rsvd4;
                        sb.Append(Utils.convertToHex(res.rsvd4));
                        break;
                        
                    case "m4xm":
                        _ = int.TryParse(property.Value.ToString(), out int m4xm);
                        res.m4xm= m4xm; 
                        sb.Append(Utils.convertToHex(res.m4xm));
                        break;
                    case "m4ym":
                        _ = int.TryParse(property.Value.ToString(), out int m4ym);
                        res.m4xm = m4ym;
                        sb.Append(Utils.convertToHex(res.m4ym)); 
                        break;
                    case "m4zm":
                        _ = int.TryParse(property.Value.ToString(), out int m4zm);
                        res.m4zm = m4zm;    
                        sb.Append(Utils.convertToHex(res.m4zm));
                        break;
                    case "m4status":
                        _ = byte.TryParse(property.Value.ToString(), out byte m4staus);
                        res.m4status = m4staus;  
                        sb.Append(Utils.convertToHex(res.m4adm));
                        break;
                    case "m4adm":
                        byte m4adm = (byte)Utils.ConvertBinaryStringToUInt32("00" + property.Value.ToString());
                        res.m4adm = m4adm;  
                        sb.Append(res.m4adm.ToString("X2"));
                        break;
                    case "rsvd5":
                        byte[] rsvd5 = new byte[] { 0, 0 };
                        res.rsvd6 = rsvd5;
                        sb.Append(Utils.convertToHex(res.rsvd5));
                        break;

                    case "m5xm":
                        _ = int.TryParse(property.Value.ToString(), out int m5xm);
                        res.m5xm = m5xm;
                        sb.Append(Utils.convertToHex(res.m5xm)); 
                        break;
                    case "m5ym":
                        _ = int.TryParse(property.Value.ToString(), out int m5ym);
                        res.m5ym = m5ym;    
                        sb.Append(Utils.convertToHex(res.m5ym));
                        break;
                    case "m5zm":
                        _ = int.TryParse(property.Value.ToString(), out int m5zm);
                        res.m5zm = m5zm;
                        sb.Append(Utils.convertToHex(res.m5zm));
                        break;
                    case "m5status":                        
                        _ = byte.TryParse(property.Value.ToString(), out byte m5staus);
                        res.m5status = m5staus;
                        sb.Append(Utils.convertToHex(res.m5status)); 
                        break;
                    case "m5adm":
                        byte m5adm = (byte)Utils.ConvertBinaryStringToUInt32("00" + property.Value.ToString());
                        res.m5adm = m5adm;  
                        sb.Append(Utils.convertToHex(res.m5adm));
                        break;
                    case "rsvd6":
                         byte[] rsvd6 = new byte[] { 0, 0 };
                        res.rsvd6 = rsvd6;
                        sb.Append(Utils.convertToHex(res.rsvd6));
                        break;

                    case "m6xm":
                        _ = int.TryParse(property.Value.ToString(), out int m6xm);
                        res.m6xm = m6xm;    
                        sb.Append(Utils.convertToHex(res.m6xm));
                        break;
                    case "m6ym":
                        _ = int.TryParse(property.Value.ToString(), out int m6ym);
                        res.m6ym = m6ym;    
                        sb.Append(Utils.convertToHex(res.m6ym));
                        break;
                    case "m6zm":
                        _ = int.TryParse(property.Value.ToString(), out int m6zm);
                        res.m6zm = m6zm;    
                        sb.Append(Utils.convertToHex(res.m6zm));
                        break;
                    case "m6status":
                        _ = byte.TryParse(property.Value.ToString(), out byte m6staus);
                        res.m6status = m6staus;  
                        sb.Append(Utils.convertToHex(res.m6status));
                        break;
                    case "m6adm":
                        byte m6adm = (byte)Utils.ConvertBinaryStringToUInt32("00" + property.Value.ToString()); 
                        res.m6adm = m6adm;  
                        sb.Append(Utils.convertToHex(res.m6adm));
                        break;
                    case "checksum":
                        res.checksum = res.calculateChecksum();
                        sb.Append(res.checksum);
                        break;
                    case "rsvd7":
                        byte rsvd7 = 0 ;
                        res.rsvd7 = rsvd7;
                        sb.Append(Utils.convertToHex(res.rsvd7));
                        break;
                        
                    case "footer":
                        var foot = property.Value.ToString();
                        for (var i = 0; i < foot.Length; i++)
                        {
                            res.footer[i] = foot[i];
                            sb.Append(Convert.ToByte(res.footer[i]).ToString("X2")); //get hex of char
                        }
                        break;
                }
            }                       
            await SavebyteArrayToFile(sb.ToString(),res);
            return Ok();
        }

        /// <summary>
        /// send byte array to socket
        /// </summary>
        /// <param name="inputArray"></param>
        private void configSocket(byte[] inputArray)
        {

            Socket client = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipaddr = IPAddress.Parse("192.168.1.15");
            int PortInput = 7;
            //IPAddress ipaddr = IPAddress.Parse("127.0.0.1");
            // int PortInput = 6060;
            try
            {
                _logger.LogInformation($"{DateTime.Now}");
                Console.WriteLine(string.Format("IPAddress: {0} - Port: {1}", ipaddr.ToString(), PortInput));
                client.Connect(ipaddr, PortInput);
                client.Send(inputArray);
                byte[] buffReceived = new byte[108];
                int nRecv = client.Receive(buffReceived);
                Console.WriteLine("Data received: {0}", Encoding.ASCII.GetString(buffReceived, 0, nRecv));            
            }
            catch (Exception excp)
            {
                Console.WriteLine(excp.ToString());
            }
            finally
            {
                if (client != null)
                {
                    if (client.Connected)
                    {
                        client.Shutdown(SocketShutdown.Both);
                    }
                    client.Close();
                    client.Dispose();
                }
            }
        }
    }
    
}
