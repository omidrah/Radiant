using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
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
        AskResult res;
        public HomeController(ILogger<HomeController> logger)
        {
             res = new AskResult();
            _logger = logger;
        }
        int num = 1;
        // GET: HomeController
        [HttpGet]
        public bool Index()
        {           
            return true;
        }
        /// <summary>
        /// ورودی رشته دریافت شده  و  باینری آن در فایل چاب می شود
        /// </summary>
        /// <param name="inputString"></param>
        private void SaveStringToFile(string inputString)
        {
            byte[] binaryBytes = Encoding.ASCII.GetBytes(inputString);
            // Convert each byte to its binary representation
            StringBuilder binaryStringBuilder = new StringBuilder();
            foreach (byte b in binaryBytes)
            {
                binaryStringBuilder.AppendFormat("{0:B}", Convert.ToString(b, 2).PadLeft(8, '0'));
            }

            string binaryRepresentation = binaryStringBuilder.ToString();

            Console.WriteLine($"Binary representation of \"{inputString}\": {binaryRepresentation}");
            Console.WriteLine($" \n count of {binaryRepresentation.Length}");
            // Create a new stream to write to the file
            BinaryWriter Writer = new BinaryWriter(System.IO.File.OpenWrite("d:\\a.txt")); ;
            // Writer raw data                
            Writer.Write(binaryRepresentation);
            Writer.Flush();
            Writer.Close();
        }
        /// <summary>
        /// آرایه ای از بایت ها دریافت شده  و باینری آن در فایل چاپ میشود
        /// </summary>
        /// <param name="inputString"></param>
        private async Task SavebyteArrayToFile(AskResult inputString)
         {
            var tmp = res.ToString();  
            //var cnt =  res.toByteArray().Length;    
            //Console.WriteLine($"data representation of \"{inputString}\": {inputString}");
            configSocket(tmp);

            // Start async Task to Save Image
            var namefile = DateTime.Now.ToString("yyyy-dd-M-HH-mm-ss");
            await FileWriteAsync($"d:\\{namefile}.txt", tmp);
        }

        private async Task FileWriteAsync(string filePath, string message, bool append = false)
        {
            try
            {
                using (FileStream stream = new FileStream(filePath, append ? FileMode.Append : FileMode.Create, FileAccess.Write, FileShare.None, 4096, true))
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
        [HttpPost("saveData")]
        public async Task<IActionResult> saveData([FromBody] JsonObject inputString)
        {

            //_logger.LogInformation("***Enter saving***\n");
            //_logger.LogInformation("******************\n");
            //_logger.LogInformation($"{inputString}");
            //_logger.LogInformation("******************\n");

            
            // Access values by key
            foreach (var property in inputString)
            {
                string key = property.Key;
                switch (key)
                {
                    case "header":
                        string head = property.Value.ToString();
                        //byte[] header = Encoding.ASCII.GetBytes(head);
                        //Array.Copy(header, 0, blockdata, 0, 3);
                        for (var i = 0; i < head.Length; i++)
                        {
                            res.header[i]= head[i]; 
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
                        break;
                    case "att":
                        if (string.IsNullOrEmpty(property.Value.ToString()))
                        {
                            _ = byte.TryParse(property.Value.ToString(), out byte att);
                            res.att = att;
                        }
                        break;
                    case "mfreq":
                        if (string.IsNullOrEmpty(property.Value.ToString()))
                        {
                            _ = byte.TryParse(property.Value.ToString(), out byte mfreq);
                            res.mfreq=  mfreq;//frequenty
                        }
                        break;
                    case "m1xm":
                        _ = Int32.TryParse(property.Value.ToString(), out Int32 m1xm);
                        //byte[] M1_Xm = BitConverter.GetBytes(m1xm); //convert int to byteArray
                        //Array.Copy(M1_Xm, 0, blockdata, 7, 4);
                        res.m1xm = m1xm;
                        break;
                    case "m1ym":
                        _= Int32.TryParse(property.Value.ToString(), out Int32 m1ym);
                        res.m1ym = m1ym;    
                        break;
                    case "m1zm":
                        _ = Int32.TryParse(property.Value.ToString(), out Int32 m1zm);
                       res.m1zm = m1zm;
                        break;
                    case "m1staus":
                        _ = byte.TryParse(property.Value.ToString(), out byte m1staus);
                        res.m1staus= m1staus;
                        break;
                    case "m1adm":
                        byte.TryParse(property.Value.ToString(), out byte m1adm);
                        res.m1adm = m1adm;
                        break;
                   
                    case "m2xm":
                        _ = Int32.TryParse(property.Value.ToString(), out Int32 m2xm);
                        res.m2xm = m2xm;
                        break;
                    case "m2ym":                      
                        _ = Int32.TryParse(property.Value.ToString(), out Int32 m2ym);
                        res.m2ym = m2ym;
                        break;
                    case "m2zm":
                        _ = Int32.TryParse(property.Value.ToString(), out Int32 m2zm);
                        res.m2ym= m2zm;
                        break;
                    case "m2staus":
                        _ = byte.TryParse(property.Value.ToString(), out byte m2staus);
                        res.m2staus = m2staus;  
                        break;
                    case "m2adm":
                        _ = byte.TryParse(property.Value.ToString(), out byte m2adm);
                        res.m6xm = m2adm;
                        break;
                    
                    case "m3xm":
                        _ = Int32.TryParse(property.Value.ToString(), out Int32 m3xm);
                        res.m3xm = m3xm;    
                        break;
                    case "m3ym":
                        _ = Int32.TryParse(property.Value.ToString(), out Int32 m3ym);
                        res.m3ym = m3ym;    
                        break;
                    case "m3zm":
                        _ = Int32.TryParse(property.Value.ToString(), out Int32 m3zm);
                        res.m3zm = m3zm;
                        break;
                    case "m3staus":
                        _ = byte.TryParse(property.Value.ToString(), out byte m3staus);
                        res.m3staus=        m3staus;    
                        break;
                    case "m3adm":                        
                        _ = byte.TryParse(property.Value.ToString(), out byte m3adm);
                        res.m3adm = m3adm;  
                        break;
                    
                    case "m4xm":
                        _ = Int32.TryParse(property.Value.ToString(), out Int32 m4xm);
                        res.m4xm= m4xm; 
                        break;
                    case "m4ym":
                        _ = Int32.TryParse(property.Value.ToString(), out Int32 m4ym);
                        res.m4xm = m4ym;
                        break;
                    case "m4zm":
                        _ = Int32.TryParse(property.Value.ToString(), out Int32 m4zm);
                        res.m4zm = m4zm;    
                        break;
                    case "m4staus":
                        _ = byte.TryParse(property.Value.ToString(), out byte m4staus);
                        res.m4staus = m4staus;  
                        break;
                    case "m4adm":
                        _ = byte.TryParse(property.Value.ToString(), out byte m4adm);
                        res.m4adm = m4adm;  
                        break;

                    case "m5xm":
                        _ = Int32.TryParse(property.Value.ToString(), out Int32 m5xm);
                        res.m5xm = m5xm;
                        break;
                    case "m5ym":
                        _ = Int32.TryParse(property.Value.ToString(), out Int32 m5ym);
                        res.m5ym = m5ym;    
                        break;
                    case "m5zm":
                        _ = Int32.TryParse(property.Value.ToString(), out Int32 m5zm);
                        res.m5zm = m5zm;
                        break;
                    case "m5staus":                        
                        _ = byte.TryParse(property.Value.ToString(), out byte m5staus);
                        res.m5staus = m5staus;
                        break;
                    case "m5adm":
                        _ = byte.TryParse(property.Value.ToString(), out byte m5adm);
                        res.m5adm = m5adm;  
                        break;

                    case "m6xm":
                        _ = Int32.TryParse(property.Value.ToString(), out Int32 m6xm);
                        res.m6xm = m6xm;    
                        break;
                    case "m6ym":
                        _ = Int32.TryParse(property.Value.ToString(), out Int32 m6ym);
                        res.m6ym = m6ym;    
                        break;
                    case "m6zm":
                        _ = Int32.TryParse(property.Value.ToString(), out Int32 m6zm);
                        res.m6zm = m6zm;    
                        break;
                    case "m6staus":
                        _ = byte.TryParse(property.Value.ToString(), out byte m6staus);
                        res.m6staus = m6staus;  
                        break;
                    case "m6adm":
                        _ = byte.TryParse(property.Value.ToString(), out byte m6adm);
                        res.m6adm = m6adm;  
                        break;
                    case "footer":
                        var foot = property.Value.ToString();
                        for (var i = 0; i < foot.Length; i++)
                        {
                            res.footer[i] = foot[i];
                        }
                        break;
                }
            }                   
            
            await SavebyteArrayToFile(res);

            return Ok();
        }
        /// <summary>
        /// send byte array to socket
        /// </summary>
        /// <param name="inputCommand"></param>
        private void configSocket(string inputCommand)
        {
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipaddr = IPAddress.Parse("192.168.1.10");
            int PortInput = 7;
            //IPAddress ipaddr = IPAddress.Parse("127.0.0.1");
            //int PortInput = 6060;
            //try
            //{               
                System.Console.WriteLine(string.Format("IPAddress: {0} - Port: {1}", ipaddr.ToString(), PortInput));
                client.Connect(ipaddr, PortInput);
                Console.WriteLine("Connected to the server, type text and press enter to send it to the srever, type <EXIT> to close.");
                //while (true)
                //{
                    client.Send (Encoding.ASCII.GetBytes(inputCommand));
                    byte[] buffReceived = new byte[1024];
                    int nRecv = client.Receive(buffReceived);
                    Console.WriteLine("Data received: {0}", Encoding.ASCII.GetString(buffReceived, 0, nRecv));
                //}
            //}
            //catch (Exception excp)
            //{
            //    Console.WriteLine(excp.ToString());
            //}
            //finally
            //{
            //    if (client != null)
            //    {
            //        if (client.Connected)
            //        {
            //            client.Shutdown(SocketShutdown.Both);
            //        }
            //        client.Close();
            //        client.Dispose();
            //    }
            //}
        }
    }
    
}
