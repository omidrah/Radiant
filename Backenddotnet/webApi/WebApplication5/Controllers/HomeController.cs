using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json.Nodes;

namespace WebApplication5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        int num = 1;
        // GET: HomeController
        public bool Index()
        {
            byte[] blockdata = new byte[88];
            var a = "CMD";

            byte[] header = Encoding.UTF8.GetBytes(a);
            //Array.Resize(ref header, 3);
            Array.Copy(header, 0, blockdata, 0, 3);

            blockdata[3] = 0x00; //Testmode
            blockdata[4] = 0x00;//downdatamode
            blockdata[5] = 0x00;//att value
            blockdata[6] = 0x01;//frequenty

            byte[] M1_Xm = new byte[3] { 0x00, 0x00, 0x00 };
            Array.Copy(M1_Xm, 0, blockdata, 7, 3);
            byte[] M1_Ym = new byte[3] { 0x00, 0x00, 0x00 };
            Array.Copy(M1_Ym, 0, blockdata, 10, 3);
            byte[] M1_Zm = new byte[3] { 0x00, 0x00, 0x00 };
            Array.Copy(M1_Zm, 0, blockdata, 13, 3);
            byte[] M1_Status = new byte[1] { 0x00 };
            Array.Copy(M1_Status, 0, blockdata, 16, 1);
            byte[] M1_Adm = new byte[1] { 0x0D };
            Array.Copy(M1_Adm, 0, blockdata, 17, 1);

            byte[] M2_Xm = new byte[3] { 0x00, 0x00, 0x00 };
            Array.Copy(M2_Xm, 0, blockdata, 18, 3);
            byte[] M2_Ym = new byte[3] { 0x00, 0x00, 0x00 };
            Array.Copy(M2_Ym, 0, blockdata, 21, 3);
            byte[] M2_Zm = new byte[3] { 0x00, 0x00, 0x00 };
            Array.Copy(M2_Zm, 0, blockdata, 24, 3);
            byte[] M2_Status = new byte[1] { 0x00 };
            Array.Copy(M2_Status, 0, blockdata, 27, 1);
            byte[] M2_Adm = new byte[1] { 0x0E };
            Array.Copy(M2_Adm, 0, blockdata, 28, 1);

            byte[] M3_Xm = new byte[3] { 0x00, 0x00, 0x00 };
            Array.Copy(M3_Xm, 0, blockdata, 29, 3);
            byte[] M3_Ym = new byte[3] { 0x00, 0x00, 0x00 };
            Array.Copy(M3_Ym, 0, blockdata, 32, 3);
            byte[] M3_Zm = new byte[3] { 0x00, 0x00, 0x00 };
            Array.Copy(M3_Zm, 0, blockdata, 35, 3);
            byte[] M3_Status = new byte[1] { 0x00 };
            Array.Copy(M3_Status, 0, blockdata, 38, 1);
            byte[] M3_Adm = new byte[1] { 0x15 };
            Array.Copy(M3_Adm, 0, blockdata, 39, 1);


            byte[] M4_Xm = new byte[3] { 0x00, 0x00, 0x00 };
            Array.Copy(M4_Xm, 0, blockdata, 40, 3);
            byte[] M4_Ym = new byte[3] { 0x00, 0x00, 0x00 };
            Array.Copy(M4_Ym, 0, blockdata, 43, 3);
            byte[] M4_Zm = new byte[3] { 0x00, 0x00, 0x00 };
            Array.Copy(M4_Zm, 0, blockdata, 46, 3);
            byte[] M4_Status = new byte[1] { 0x00 };
            Array.Copy(M4_Status, 0, blockdata, 49, 1);
            byte[] M4_Adm = new byte[1] { 0x1C };
            Array.Copy(M4_Adm, 0, blockdata, 50, 1);


            byte[] M5_Xm = new byte[3] { 0x00, 0x00, 0x00 };
            Array.Copy(M5_Xm, 0, blockdata, 51, 3);
            byte[] M5_Ym = new byte[3] { 0x00, 0x00, 0x00 };
            Array.Copy(M5_Ym, 0, blockdata, 54, 3);
            byte[] M5_Zm = new byte[3] { 0x00, 0x00, 0x00 };
            Array.Copy(M5_Zm, 0, blockdata, 57, 3);
            byte[] M5_Status = new byte[1] { 0x00 };
            Array.Copy(M5_Status, 0, blockdata, 60, 1);
            byte[] M5_Adm = new byte[1] { 0x16 };
            Array.Copy(M5_Adm, 0, blockdata, 61, 1);

            byte[] M6_Xm = new byte[3] { 0x00, 0x00, 0x00 };
            Array.Copy(M6_Xm, 0, blockdata, 62, 3);
            byte[] M6_Ym = new byte[3] { 0x00, 0x00, 0x00 };
            Array.Copy(M6_Ym, 0, blockdata, 65, 3);
            byte[] M6_Zm = new byte[3] { 0x00, 0x00, 0x00 };
            Array.Copy(M6_Zm, 0, blockdata, 68, 3);
            byte[] M6_Status = new byte[1] { 0x00 };
            Array.Copy(M6_Status, 0, blockdata, 71, 1);
            byte[] M6_Adm = new byte[1] { 0x19 };
            Array.Copy(M6_Adm, 0, blockdata, 72, 1);


            byte[] reverse = new byte[10] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            Array.Copy(reverse, 0, blockdata, 73, 10);

            byte[] checksum = new byte[2] { 0x00, 0x00 };
            Array.Copy(checksum, 0, blockdata, 83, 2);

            byte[] footer = new byte[3] { 0x00, 0x00, 0x00 };
            Array.Copy(footer, 0, blockdata, 85, 3);
            SavebyteArrayToFile(blockdata);
            return true;
        }
        /// <summary>
        /// ورودی رشته دریافت شده  و  باینری آن در فایل چاب می شود
        /// </summary>
        /// <param name="inputString"></param>
        protected void SaveStringToFile(string inputString)
        {
            byte[] binaryBytes = Encoding.UTF8.GetBytes(inputString);
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
        protected async Task SavebyteArrayToFile(byte[] inputString)
        {
            // Convert each byte to its binary representation
            StringBuilder binaryStringBuilder = new StringBuilder();
            foreach (byte b in inputString)
            {
                binaryStringBuilder.AppendFormat("{0:B}", Convert.ToString(b, 2).PadLeft(8, '0'));
            }
            string binaryRepresentation = binaryStringBuilder.ToString();
            Console.WriteLine($"Binary representation of \"{inputString}\": {binaryRepresentation}");
            configSocket(binaryRepresentation);

            // Create a new stream to write to the file
            //BinaryWriter Writer = new BinaryWriter(System.IO.File.OpenWrite($"d:\\a{num}.txt"));
            // Writer raw data                
            //Writer.Write(binaryRepresentation);
            //Writer.Flush();
            //Writer.Close();

            // Start async Task to Save Image
            var namefile = DateTime.Now.ToString("yyyy-dd-M-HH-mm-ss");
            await System.IO.File.WriteAllTextAsync($"d:\\{namefile}.txt", binaryRepresentation);
        }
        [HttpPost("saveData")]
        public async Task<IActionResult> saveData([FromBody] JsonObject inputString)
        {

            _logger.LogInformation("***Enter saving***\n");
            _logger.LogInformation("******************\n");
            _logger.LogInformation($"{inputString}");
            _logger.LogInformation("******************\n");

            byte[] blockdata = new byte[88];
            blockdata[5] = (byte)0;//att value

            /*cmd tab*/
            var datamode = 0;var testmode=0;
            // Access values by key
            foreach (var property in inputString)
            {
                string key = property.Key;
                switch (key)
                {
                    case "header":
                        var head = property.Value.ToString();
                        byte[] header = Encoding.UTF8.GetBytes(head);
                        Array.Copy(header, 0, blockdata, 0, 3);
                        break;
                    case "footer":
                        var foot = property.Value.ToString();
                        byte[] footer = Encoding.UTF8.GetBytes(foot);
                        Array.Copy(footer, 0, blockdata, 85, 3);
                        break;
                    case "datamode":
                        switch (property.Value.ToString())
                        {
                            case "manual":
                                datamode = 1;
                                break;
                            case "simulate":
                                datamode = 2;
                             break;
                            case "loopback":
                                datamode = 0;
                                break;
                        }
                        blockdata[4] = (byte)datamode;//downdatamode 0

                        break;
                    case "testmode":
                        switch (property.Value.ToString())
                        {
                            case "txoff":
                                testmode = 1;
                                break;
                            case "directmds":
                                testmode = 2;
                                break;
                            case "directpwr":
                                testmode = 0;
                                break;
                        }
                        blockdata[3] = (byte)testmode; //Testmode 1
                        break;
                    case "mfreq":
                        _ = int.TryParse(property.Value.ToString(), out int mfreq);
                        blockdata[6] = (byte)mfreq;//frequenty
                        break;

                    case "m1xm":
                        _ = int.TryParse(property.Value.ToString(), out int m1xm);
                        byte[] M1_Xm = new byte[3];
                        M1_Xm[0] = (byte)(m1xm >> 16); // get the first 8 bits
                        M1_Xm[1] = (byte)(m1xm >> 8);  // get the next 8 bits
                        M1_Xm[2] = (byte)m1xm;
                        Array.Copy(M1_Xm, 0, blockdata, 7, 3);
                        break;
                    case "m1ym":
                        _=int.TryParse(property.Value.ToString(), out int m1ym);
                        byte[] M1_Ym = new byte[3];
                        M1_Ym[0] = (byte)(m1ym >> 16); // get the first 8 bits
                        M1_Ym[1] = (byte)(m1ym >> 8);  // get the next 8 bits
                        M1_Ym[2] = (byte)m1ym;                        
                        Array.Copy(M1_Ym, 0, blockdata, 10, 3);
                        break;
                    case "m1zm":
                        _ = int.TryParse(property.Value.ToString(), out int m1zm);
                        byte[] M1_Zm = new byte[3];
                        M1_Zm[0] = (byte)(m1zm >> 16); // get the first 8 bits
                        M1_Zm[1] = (byte)(m1zm >> 8);  // get the next 8 bits
                        M1_Zm[2] = (byte)m1zm;
                        Array.Copy(M1_Zm, 0, blockdata, 13, 3);
                        break;
                    case "m1staus":
                        _ = int.TryParse(property.Value.ToString(), out int m1staus);
                        byte M1_Status = (byte)m1staus;
                        blockdata[16] = M1_Status;
                        break;
                    case "m1adm":
                        float.TryParse(property.Value.ToString(), out float m1adm);
                        byte M1_Adm =(byte)m1adm;
                        blockdata[17] = M1_Adm;
                        break;
                   
                    case "m2xm":
                        _ = int.TryParse(property.Value.ToString(), out int m2xm);
                        byte[] M2_Xm = new byte[3];
                        M2_Xm[0] = (byte)(m2xm >> 16); // get the first 8 bits
                        M2_Xm[1] = (byte)(m2xm >> 8);  // get the next 8 bits
                        M2_Xm[2] = (byte)m2xm;
                        Array.Copy(M2_Xm, 0, blockdata, 18, 3);
                        break;
                    case "m2ym":                      
                        _ = int.TryParse(property.Value.ToString(), out int m2ym);
                        byte[] M2_Ym = new byte[3];
                        M2_Ym[0] = (byte)(m2ym >> 16); // get the first 8 bits
                        M2_Ym[1] = (byte)(m2ym >> 8);  // get the next 8 bits
                        M2_Ym[2] = (byte)m2ym;
                        Array.Copy(M2_Ym, 0, blockdata, 21, 3);
                        break;
                    case "m2zm":
                        _ = int.TryParse(property.Value.ToString(), out int m2zm);
                        byte[] M2_Zm = new byte[3];
                        M2_Zm[0] = (byte)(m2zm >> 16); // get the first 8 bits
                        M2_Zm[1] = (byte)(m2zm >> 8);  // get the next 8 bits
                        M2_Zm[2] = (byte)m2zm;
                        Array.Copy(M2_Zm, 0, blockdata, 24, 3);
                        break;
                    case "m2staus":
                        _ = int.TryParse(property.Value.ToString(), out int m2staus);
                        byte M2_Status = (byte)m2staus;
                        blockdata[27]=M2_Status;
                        break;
                    case "m2adm":
                        _ = int.TryParse(property.Value.ToString(), out int m2adm);
                        byte M2_Adm = (byte)m2adm;
                        blockdata[28]=M2_Adm;
                        break;
                    
                    case "m3xm":
                        _ = int.TryParse(property.Value.ToString(), out int m3xm);
                        byte[] M3_Xm = new byte[3];
                        M3_Xm[0] = (byte)(m3xm >> 16); // get the first 8 bits
                        M3_Xm[1] = (byte)(m3xm >> 8);  // get the next 8 bits
                        M3_Xm[2] = (byte)m3xm;
                        Array.Copy(M3_Xm, 0, blockdata, 29, 3);
                        break;
                    case "m3ym":
                        _ = int.TryParse(property.Value.ToString(), out int m3ym);
                        byte[] M3_Ym = new byte[3];
                        M3_Ym[0] = (byte)(m3ym >> 16); // get the first 8 bits
                        M3_Ym[1] = (byte)(m3ym >> 8);  // get the next 8 bits
                        M3_Ym[2] = (byte)m3ym;
                        Array.Copy(M3_Ym, 0, blockdata, 32, 3);
                        break;
                    case "m3zm":
                        _ = int.TryParse(property.Value.ToString(), out int m3zm);
                        byte[] M3_Zm = new byte[3];
                        M3_Zm[0] = (byte)(m3zm >> 16); // get the first 8 bits
                        M3_Zm[1] = (byte)(m3zm >> 8);  // get the next 8 bits
                        M3_Zm[2] = (byte)m3zm;
                        Array.Copy(M3_Zm, 0, blockdata, 35, 3);
                        break;
                    case "m3staus":
                        _ = int.TryParse(property.Value.ToString(), out int m3staus);
                        byte M3_Status = (byte)m3staus;
                        blockdata[38] = M3_Status;
                        break;
                    case "m3adm":                        
                        _ = int.TryParse(property.Value.ToString(), out int m3adm);
                        byte M3_Adm = (byte)m3adm;
                        blockdata[39] = M3_Adm;
                        break;
                    
                    case "m4xm":
                        _ = int.TryParse(property.Value.ToString(), out int m4xm);
                        byte[] M4_Xm = new byte[3];
                        M4_Xm[0] = (byte)(m4xm >> 16); // get the first 8 bits
                        M4_Xm[1] = (byte)(m4xm >> 8);  // get the next 8 bits
                        M4_Xm[2] = (byte)m4xm;
                        Array.Copy(M4_Xm, 0, blockdata, 40, 3);
                        break;
                    case "m4ym":
                        _ = int.TryParse(property.Value.ToString(), out int m4ym);
                        byte[] M4_Ym = new byte[3];
                        M4_Ym[0] = (byte)(m4ym >> 16); // get the first 8 bits
                        M4_Ym[1] = (byte)(m4ym >> 8);  // get the next 8 bits
                        M4_Ym[2] = (byte)m4ym;
                        Array.Copy(M4_Ym, 0, blockdata, 43, 3);
                        break;
                    case "m4zm":
                        _ = int.TryParse(property.Value.ToString(), out int m4zm);
                        byte[] M4_Zm = new byte[3];
                        M4_Zm[0] = (byte)(m4zm >> 16); // get the first 8 bits
                        M4_Zm[1] = (byte)(m4zm >> 8);  // get the next 8 bits
                        M4_Zm[2] = (byte)m4zm;
                        Array.Copy(M4_Zm, 0, blockdata, 46, 3);
                        break;
                    case "m4staus":
                        _ = int.TryParse(property.Value.ToString(), out int m4staus);
                        byte M4_Status = (byte)m4staus;
                        blockdata[49] = M4_Status;
                        break;
                    case "m4adm":
                        _ = int.TryParse(property.Value.ToString(), out int m4adm);
                        byte M4_Adm = (byte)m4adm;
                        blockdata[50] = M4_Adm;
                        break;

                    case "m5xm":
                        _ = int.TryParse(property.Value.ToString(), out int m5xm);
                        byte[] M5_Xm = new byte[3];
                        M5_Xm[0] = (byte)(m5xm >> 16); // get the first 8 bits
                        M5_Xm[1] = (byte)(m5xm >> 8);  // get the next 8 bits
                        M5_Xm[2] = (byte)m5xm;
                        Array.Copy(M5_Xm, 0, blockdata, 51, 3);
                        break;
                    case "m5ym":
                        _ = int.TryParse(property.Value.ToString(), out int m5ym);
                        byte[] M5_Ym = new byte[3];
                        M5_Ym[0] = (byte)(m5ym >> 16); // get the first 8 bits
                        M5_Ym[1] = (byte)(m5ym >> 8);  // get the next 8 bits
                        M5_Ym[2] = (byte)m5ym;
                        Array.Copy(M5_Ym, 0, blockdata, 54, 3);
                        break;
                    case "m5zm":
                        _ = int.TryParse(property.Value.ToString(), out int m5zm);
                        byte[] M5_Zm = new byte[3];
                        M5_Zm[0] = (byte)(m5zm >> 16); // get the first 8 bits
                        M5_Zm[1] = (byte)(m5zm >> 8);  // get the next 8 bits
                        M5_Zm[2] = (byte)m5zm;
                        Array.Copy(M5_Zm, 0, blockdata, 57, 3);
                        break;
                    case "m5staus":                        
                        _ = int.TryParse(property.Value.ToString(), out int m5staus);
                        byte M5_Status = (byte)m5staus;
                        blockdata[60] = M5_Status;
                        break;
                    case "m5adm":
                        _ = int.TryParse(property.Value.ToString(), out int m5adm);
                        byte M5_Adm = (byte)m5adm;
                        blockdata[61] = M5_Adm;
                        break;

                    case "m6xm":
                        _ = int.TryParse(property.Value.ToString(), out int m6xm);
                        byte[] M6_Xm = new byte[3];
                        M6_Xm[0] = (byte)(m6xm >> 16); // get the first 8 bits
                        M6_Xm[1] = (byte)(m6xm >> 8);  // get the next 8 bits
                        M6_Xm[2] = (byte)m6xm;
                        Array.Copy(M6_Xm, 0, blockdata, 62, 3);
                        break;
                    case "m6ym":
                        _ = int.TryParse(property.Value.ToString(), out int m6ym);
                        byte[] M6_Ym = new byte[3];
                        M6_Ym[0] = (byte)(m6ym >> 16); // get the first 8 bits
                        M6_Ym[1] = (byte)(m6ym >> 8);  // get the next 8 bits
                        M6_Ym[2] = (byte)m6ym;
                        Array.Copy(M6_Ym, 0, blockdata, 65, 3);
                        break;
                    case "m6zm":
                        _ = int.TryParse(property.Value.ToString(), out int m6zm);
                        byte[] M6_Zm = new byte[3];
                        M6_Zm[0] = (byte)(m6zm >> 16); // get the first 8 bits
                        M6_Zm[1] = (byte)(m6zm >> 8);  // get the next 8 bits
                        M6_Zm[2] = (byte)m6zm;
                        Array.Copy(M6_Zm, 0, blockdata, 68, 3);
                        break;
                    case "m6staus":
                        _ = int.TryParse(property.Value.ToString(), out int m6staus);
                        byte M6_Status = (byte)m6staus;
                        blockdata[70] = M6_Status;
                        break;
                    case "m6adm":
                        _ = int.TryParse(property.Value.ToString(), out int m6adm);
                        byte M6_Adm = (byte)m6adm;
                        blockdata[71] = M6_Adm;
                        break;

                }
            }

            byte[] reverse = new byte[10];
            for (int i = 0; i < reverse.Length; i++) //set default to zere
            {
                reverse[i] = (byte)0;
            }
            Array.Copy(reverse, 0, blockdata, 73, 10);

            byte[] checksum = new byte[2];
            checksum[0] = (byte)0; checksum[1]=(byte)0;
            Array.Copy(checksum, 0, blockdata, 83, 2);

           
            await SavebyteArrayToFile(blockdata);

            return Ok();
        }
        /// <summary>
        /// send byte array to socket
        /// </summary>
        /// <param name="inputCommand"></param>
        public void configSocket(string inputCommand)
        {
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //IPAddress ipaddr = IPAddress.Parse("192.168.1.1");
            //int PortInput = 7;

            IPAddress ipaddr = IPAddress.Parse("127.0.0.1");
            int PortInput = 6060;
            try
            {
               
                System.Console.WriteLine(string.Format("IPAddress: {0} - Port: {1}", ipaddr.ToString(), PortInput));
                client.Connect(ipaddr, PortInput);
                Console.WriteLine("Connected to the server, type text and press enter to send it to the srever, type <EXIT> to close.");
                while (true)
                {
                    client.Send (Encoding.UTF8.GetBytes(inputCommand));

                    byte[] buffReceived = new byte[1024];
                    int nRecv = client.Receive(buffReceived);

                    Console.WriteLine("Data received: {0}", Encoding.UTF8.GetString(buffReceived, 0, nRecv));
                }
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
