﻿using Microsoft.AspNetCore.Mvc;
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
        public HomeController(ILogger<HomeController> logger)
        {
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
        private async Task SavebyteArrayToFile(string  hexValue,AskResult valueFromUi)
         {
            //var cnt =  res.toByteArray().Length;    
            Console.WriteLine($"Value from ui is {valueFromUi} ");
            Console.WriteLine($"\n Value in Hex {hexValue}");
            configSocket(hexValue);

            // Start async Task to Save Image
            var namefile = DateTime.Now.ToString("yyyy-dd-M-HH-mm-ss");
            await FileWriteAsync($"d:\\{namefile}.txt", valueFromUi.ToString() + "\n"+ hexValue);
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
            // Access values by key
            var res = new AskResult();
            StringBuilder sb = new(); 
            byte[] buff = new byte[108];
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
                            sb.Append(Convert.ToByte(res.header[i]).ToString("X2"));                            
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
                            _ = byte.TryParse(property.Value.ToString(), out byte rsvd1);
                            res.rsvd1 = rsvd1;
                            sb.Append(convertToHex(res.rsvd1));                        
                        break;
                    case "m1xm":
                        _ = Int32.TryParse(property.Value.ToString(), out Int32 m1xm);
                        //byte[] M1_Xm = BitConverter.GetBytes(m1xm); //convert int to byteArray
                        //Array.Copy(M1_Xm, 0, blockdata, 7, 4);
                        res.m1xm = m1xm;
                        //string result = convertToHex(res.m1xm);
                        sb.Append(convertToHex(res.m1xm));
                        break;
                    case "m1ym":
                        _= int.TryParse(property.Value.ToString(), out int m1ym);
                        res.m1ym = m1ym;
                        sb.Append(convertToHex(res.m1ym));
                        break;
                    case "m1zm":
                        _ = int.TryParse(property.Value.ToString(), out int m1zm);
                       res.m1zm = m1zm;
                        sb.Append(convertToHex(res.m1zm));
                        break;
                    case "m1status":
                        _ = byte.TryParse(property.Value.ToString(), out byte m1staus);
                        res.m1status = m1staus;
                        sb.Append(convertToHex(res.m1status));
                        break;
                    case "m1adm":
                        _ = byte.TryParse(property.Value.ToString(), out byte m1adm);
                        res.m1adm = m1adm;
                        sb.Append(convertToHex(res.m1adm));
                        break;
                    case "rsvd2":
                        byte[] rsvd2 = new byte[] { 0, 0 };
                        res.rsvd2 = rsvd2;
                        sb.Append(convertToHex(res.rsvd2));
                        break;
                    case "m2xm":
                        _ = int.TryParse(property.Value.ToString(), out int m2xm);
                        res.m2xm = m2xm;
                        sb.Append(convertToHex(res.m2xm));
                        break;
                    case "m2ym":                      
                        _ = int.TryParse(property.Value.ToString(), out int m2ym);
                        res.m2ym = m2ym;
                        sb.Append(convertToHex(res.m2ym));
                        break;
                    case "m2zm":
                        _ = int.TryParse(property.Value.ToString(), out int m2zm);
                        res.m2ym= m2zm;
                        sb.Append(convertToHex(res.m2zm));
                        break;
                    case "m2status":
                        _ = byte.TryParse(property.Value.ToString(), out byte m2staus);
                        res.m2status = m2staus;
                        sb.Append(res.m2status.ToString("X2")); 
                        break;
                    case "m2adm":
                        _ = byte.TryParse(property.Value.ToString(), out byte m2adm);
                        res.m6xm = m2adm;
                        sb.Append(res.m2adm.ToString("X2")); 
                        break;
                    case "rsvd3":
                        byte[] rsvd3 = new byte[] { 0, 0 };
                        res.rsvd3 = rsvd3;
                        sb.Append(convertToHex(res.rsvd3));
                        break;
                    case "m3xm":
                        _ = int.TryParse(property.Value.ToString(), out int m3xm);
                        res.m3xm = m3xm;    
                        sb.Append(convertToHex(res.m3xm)); 
                        break;
                    case "m3ym":
                        _ = int.TryParse(property.Value.ToString(), out int m3ym);
                        res.m3ym = m3ym;    
                        sb.Append(convertToHex(res.m3ym));
                        break;
                    case "m3zm":
                        _ = int.TryParse(property.Value.ToString(), out int m3zm);
                        res.m3zm = m3zm;
                        sb.Append(convertToHex(res.m3zm));
                        break;
                    case "m3status":
                        _ = byte.TryParse(property.Value.ToString(), out byte m3staus);
                        res.m3status=  m3staus;    
                        sb.Append(res.m3status.ToString("X2"));
                        break;
                    case "m3adm":                        
                        _ = byte.TryParse(property.Value.ToString(), out byte m3adm);
                        res.m3adm = m3adm;  
                        sb.Append(res.m3adm.ToString("X2"));
                        break;
                    case "rsvd4":
                        byte[] rsvd4 = new byte[] { 0, 0 };
                        res.rsvd4 = rsvd4;
                        sb.Append(convertToHex(res.rsvd4));
                        break;
                    case "m4xm":
                        _ = int.TryParse(property.Value.ToString(), out int m4xm);
                        res.m4xm= m4xm; 
                        sb.Append(convertToHex(res.m4xm));
                        break;
                    case "m4ym":
                        _ = int.TryParse(property.Value.ToString(), out int m4ym);
                        res.m4xm = m4ym;
                        sb.Append(convertToHex(res.m4ym)); 
                        break;
                    case "m4zm":
                        _ = int.TryParse(property.Value.ToString(), out int m4zm);
                        res.m4zm = m4zm;    
                        sb.Append(convertToHex(res.m4zm));
                        break;
                    case "m4status":
                        _ = byte.TryParse(property.Value.ToString(), out byte m4staus);
                        res.m4status = m4staus;  
                        sb.Append(res.m4status.ToString("X2"));
                        break;
                    case "m4adm":
                        _ = byte.TryParse(property.Value.ToString(), out byte m4adm);
                        res.m4adm = m4adm;  
                        sb.Append(res.m4adm.ToString("X2"));
                        break;
                    case "rsvd5":
                        byte[] rsvd5 = new byte[] { 0, 0 };
                        res.rsvd6 = rsvd5;
                        sb.Append(convertToHex(res.rsvd5));
                        break;
                    case "m5xm":
                        _ = int.TryParse(property.Value.ToString(), out int m5xm);
                        res.m5xm = m5xm;
                        sb.Append(convertToHex(res.m5xm)); 
                        break;
                    case "m5ym":
                        _ = int.TryParse(property.Value.ToString(), out int m5ym);
                        res.m5ym = m5ym;    
                        sb.Append(convertToHex(res.m5ym));
                        break;
                    case "m5zm":
                        _ = int.TryParse(property.Value.ToString(), out int m5zm);
                        res.m5zm = m5zm;
                        sb.Append(convertToHex(res.m5zm));
                        break;
                    case "m5status":                        
                        _ = byte.TryParse(property.Value.ToString(), out byte m5staus);
                        res.m5status = m5staus;
                        sb.Append(convertToHex(res.m5status)); 
                        break;
                    case "m5adm":
                        _ = byte.TryParse(property.Value.ToString(), out byte m5adm);
                        res.m5adm = m5adm;  
                        sb.Append(convertToHex(res.m5adm));
                        break;
                    case "rsvd6":
                         byte[] rsvd6 = new byte[] { 0, 0 };
                        res.rsvd6 = rsvd6;
                        sb.Append(convertToHex(res.rsvd6));
                        break;
                    case "m6xm":
                        _ = int.TryParse(property.Value.ToString(), out int m6xm);
                        res.m6xm = m6xm;    
                        sb.Append(convertToHex(res.m6xm));
                        break;
                    case "m6ym":
                        _ = int.TryParse(property.Value.ToString(), out int m6ym);
                        res.m6ym = m6ym;    
                        sb.Append(convertToHex(res.m6ym));
                        break;
                    case "m6zm":
                        _ = int.TryParse(property.Value.ToString(), out int m6zm);
                        res.m6zm = m6zm;    
                        sb.Append(convertToHex(res.m6zm));
                        break;
                    case "m6status":
                        _ = byte.TryParse(property.Value.ToString(), out byte m6staus);
                        res.m6status = m6staus;  
                        sb.Append(convertToHex(res.m6status));
                        break;
                    case "m6adm":
                        _ = byte.TryParse(property.Value.ToString(), out byte m6adm);
                        res.m6adm = m6adm;  
                        sb.Append(convertToHex(res.m6adm));
                        break;
                    case "checksum":
                        _ = short.TryParse(property.Value.ToString(), out short checksum);
                        res.checksum = checksum;
                        sb.Append(convertToHex(res.checksum));
                        break;
                    case "rsvd7":
                        _ = byte.TryParse(property.Value.ToString(), out byte rsvd7);
                        res.rsvd7 = rsvd7;
                        sb.Append(convertToHex(res.rsvd7));
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
            
            var aa = sb.ToString();
            
            
            await SavebyteArrayToFile(sb.ToString(),res);

            return Ok();
        }
        /// <summary>
        ///  To represent Int32 as a Hexadecimal string in C#, use the ToString() method and set the base as the ToString() method’s second parameter i.e. 16 for Hexadecimal.
        /// </summary>
        /// <param name="numb"></param>
        /// <returns></returns>
        private static string convertToHex(int numb)
        {
            return string.Join("", BitConverter.GetBytes(numb).Reverse().Select(b => b.ToString("X2")));
        }
        /// <summary>
        ///  To represent short as a Hexadecimal string in C#, use the ToString() method and set the base as the ToString() method’s second parameter i.e. 16 for Hexadecimal.
        /// </summary>
        /// <param name="numb"></param>
        /// <returns></returns>
        private static string convertToHex(short numb)
        {
            return string.Join("", BitConverter.GetBytes(numb).Reverse().Select(b => b.ToString("X2")));
        }
        // <summary>
        ///  To represent byte[] as a Hexadecimal string in C#, use the ToString() method and set the base as the ToString() method’s second parameter i.e. 16 for Hexadecimal.
        /// </summary>
        /// <param name="numb"></param>
        /// <returns></returns>
        private static string convertToHex(byte[] numb)
        {
            return string.Join("", numb.Reverse().Select(b => b.ToString("X2")));
        }
        /// <summary>
        ///  To represent byte as a Hexadecimal string in C#, use the ToString() method and set the base as the ToString() method’s second parameter i.e. 16 for Hexadecimal.
        /// </summary>
        /// <param name="numb"></param>
        /// <returns></returns>
        private static string convertToHex(byte numb)
        {
            return string.Join("0x", numb.ToString("X2"));
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
                    byte[] buffReceived = new byte[108];
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
