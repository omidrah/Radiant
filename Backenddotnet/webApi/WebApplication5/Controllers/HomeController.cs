﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using System.Net.Sockets;
using System.Text;
using System.Text.Json.Nodes;
using WebApplication5.Model;
using WebApplication5.Services;
using Utils = WebApplication5.Model.Utils;

namespace WebApplication5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly Settings _settings;
        private readonly SocketService _socketService;
        private readonly FileService _fileService;
        private readonly IHubContext<DataHub> _hubContext;

        public HomeController(IHubContext<DataHub> hubContext, ILogger<HomeController> logger,
            FileService fileService, IOptions<Settings> settings,SocketService socketService)
        {
            _logger = logger;
            _fileService = fileService;
            _settings = settings.Value;
            _socketService = socketService;
            _hubContext = hubContext;
        }
        [HttpGet]
        public string Get()
        {
            return _settings.companyInfo.name; 
        }      
       
        [HttpPost("sendData")]
        public async Task<IActionResult> sendData([FromBody] JsonObject inputString)
        {            
            SendPacket res = new ();
            // Deserialize the JSON into a Packet object
            //PacketImp packet = JsonConvert.DeserializeObject<SendPacket>(inputString.ToString());
            StringBuilder sb = new(); 
            foreach (var property in inputString)
            {
                if (!string.IsNullOrEmpty(property.Value.ToString()))
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
                                    res.header[i] = head[i];
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
                        case "couple":
                                _ = byte.TryParse(property.Value.ToString(), out byte couple);
                                res.couple = couple;
                                sb.Append(res.couple.ToString("X2"));
                            break;
                        case "att":
                                _ = byte.TryParse(property.Value.ToString(), out byte att);
                                res.ATT_Value = att;
                                sb.Append(res.ATT_Value.ToString("X2"));
                            break;
                        case "mfreq":                            
                                _ = byte.TryParse(property.Value.ToString(), out byte mfreq);
                                res.frequency = mfreq;//frequenty
                                sb.Append(res.frequency.ToString("X2"));                            
                            break;
                        case "selftest":
                                _ = byte.TryParse(property.Value.ToString(), out byte selftest);
                                res.selftest = selftest;//frequenty
                                sb.Append(res.selftest.ToString("X2"));                            
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
                            _ = int.TryParse(property.Value.ToString(), out int m1ym);
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
                            byte m1adm = (byte)Utils.ConvertBinaryStringToUInt32("00" + property.Value.ToString());
                            res.m1adm = m1adm;
                            sb.Append(Utils.convertToHex(res.m1adm));
                            break;
                        case "datamode1":
                            switch (property.Value.ToString())
                            {
                                case "manual":
                                    res.downlink_data_mode1 = 1;
                                    break;
                                case "simulate":
                                    res.downlink_data_mode1 = 2;
                                    break;
                                case "loopback":
                                    res.downlink_data_mode1 = 0;
                                    break;
                            }
                            sb.Append(res.downlink_data_mode1.ToString("X2"));
                            break;
                        case "crc":
                            _ = byte.TryParse(property.Value.ToString(), out byte crc);
                            res.crc = crc;
                            sb.Append(Utils.convertToHex(res.crc));
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
                            res.m2zm = m2zm;
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
                        case "datamode2":
                            switch (property.Value.ToString())
                            {
                                case "manual":
                                    res.downlink_data_mode2 = 1;
                                    break;
                                case "simulate":
                                    res.downlink_data_mode2 = 2;
                                    break;
                                case "loopback":
                                    res.downlink_data_mode2 = 0;
                                    break;
                            }
                            sb.Append(res.downlink_data_mode2.ToString("X2"));
                            break;
                        case "cfar_coef":
                            _ = byte.TryParse(property.Value.ToString(), out byte cfar_coef);
                            res.cfar_coef = cfar_coef;
                            sb.Append(Utils.convertToHex(res.cfar_coef));
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
                            res.m3status = m3staus;
                            sb.Append(Utils.convertToHex(res.m3status));
                            break;
                        case "m3adm":
                            byte m3adm = (byte)Utils.ConvertBinaryStringToUInt32("00" + property.Value.ToString());
                            res.m3adm = m3adm;
                            sb.Append(Utils.convertToHex(res.m3adm));
                            break;
                        case "datamode3":
                            switch (property.Value.ToString())
                            {
                                case "manual":
                                    res.downlink_data_mode3 = 1;
                                    break;
                                case "simulate":
                                    res.downlink_data_mode3 = 2;
                                    break;
                                case "loopback":
                                    res.downlink_data_mode3 = 0;
                                    break;
                            }
                            sb.Append(res.downlink_data_mode3.ToString("X2"));
                            break;
                        case "rsvd4":
                            byte[] rsvd4 = new byte[] { 0 };
                            res.rsvd4 = rsvd4;
                            sb.Append(Utils.convertToHex(res.rsvd4));
                            break;

                        case "m4xm":
                            _ = int.TryParse(property.Value.ToString(), out int m4xm);
                            res.m4xm = m4xm;
                            sb.Append(Utils.convertToHex(res.m4xm));
                            break;
                        case "m4ym":
                            _ = int.TryParse(property.Value.ToString(), out int m4ym);
                            res.m4ym = m4ym;
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
                            sb.Append(Utils.convertToHex(res.m4status));
                            break;
                        case "m4adm":
                            byte m4adm = (byte)Utils.ConvertBinaryStringToUInt32("00" + property.Value.ToString());
                            res.m4adm = m4adm;
                            sb.Append(Utils.convertToHex(res.m4adm));
                            break;

                        case "datamode4":
                            switch (property.Value.ToString())
                            {
                                case "manual":
                                    res.downlink_data_mode4 = 1;
                                    break;
                                case "simulate":
                                    res.downlink_data_mode4 = 2;
                                    break;
                                case "loopback":
                                    res.downlink_data_mode4 = 0;
                                    break;
                            }
                            sb.Append(res.downlink_data_mode4.ToString("X2"));
                            break;
                        case "downlink_att":
                            _ = byte.TryParse(property.Value.ToString(), out byte downlink_att);
                            //byte[] rsvd5 = new byte[] { 0 };
                            res.downlink_att = downlink_att;
                            sb.Append(Utils.convertToHex(res.downlink_att));
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

                        case "datamode5":
                            switch (property.Value.ToString())
                            {
                                case "manual":
                                    res.downlink_data_mode5 = 1;
                                    break;
                                case "simulate":
                                    res.downlink_data_mode5 = 2;
                                    break;
                                case "loopback":
                                    res.downlink_data_mode5 = 0;
                                    break;
                            }
                            sb.Append(res.downlink_data_mode5.ToString("X2"));
                            break;
                        case "uplink_gain":
                            _ = byte.TryParse(property.Value.ToString(), out byte uplink_gain);                            
                            res.uplink_gain = uplink_gain;
                            sb.Append(Utils.convertToHex(res.uplink_gain));
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
                        case "datamode6":
                            switch (property.Value.ToString())
                            {
                                case "manual":
                                    res.downlink_data_mode6 = 1;
                                    break;
                                case "simulate":
                                    res.downlink_data_mode6 = 2;
                                    break;
                                case "loopback":
                                    res.downlink_data_mode6 = 0;
                                    break;
                            }
                            sb.Append(res.downlink_data_mode6.ToString("X2"));
                            break;
                        case "self_tester_att":
                            _ = byte.TryParse(property.Value.ToString(), out byte self_tester_att);
                            res.self_tester_att = self_tester_att;
                            sb.Append(Utils.convertToHex(res.self_tester_att));
                            break;
                        case "checksum":
                            res.checksum = res.calculateChecksum();
                            sb.Append(Utils.convertToHex(res.checksum));
                            break;
                        case "rsvd8":
                            byte rsvd8 = 0;
                            res.rsvd8 = rsvd8;
                            sb.Append(Utils.convertToHex(res.rsvd8));
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
            }
            var hexvalue = sb.ToString();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("*******************************************************************");
            Console.WriteLine($"*                    Send Packet                                  *");
            Console.WriteLine("*******************************************************************");
            Console.WriteLine($"DataSend in Decimal \n {res} \n");            
            Console.WriteLine($"DataSend in Hex     \n {hexvalue} \n");

            // Start async Task to Save send Packet To file...
            await _fileService.SaveSendPakcetAsync($"{DateTime.Now.ToString()}==> {res} \n {hexvalue}");
            /*prepare array send on socket to server*/
            var byteArray = Utils.StringToByteArray(hexvalue); //hexvalue to bytearray
            var recpacket = await _socketService.SendDataAsync(byteArray);
            /*send receieve packet from server to Ui by signalr hub*/
            if (recpacket != null)
            {
                var recpacketToUi = new RecievePacketVm
                {
                    M1_ADDR = recpacket.M1_ADDR,
                    M1_Vxm = recpacket.M1_Vxm,
                    M1_Vym = recpacket.M1_Vym,
                    M1_Vzm = recpacket.M1_Vzm,
                    M1_Status = recpacket.M1_Status,
                    M1_Vxt = recpacket.M1_Vxt,
                    M1_Vyt = recpacket.M1_Vyt,
                    M1_Vzt = recpacket.M1_Vzt,
                    M1_Xm = recpacket.M1_Xm,
                    M1_Xt = recpacket.M1_Xt,
                    M1_Ym = recpacket.M1_Ym,
                    M1_Yt = recpacket.M1_Yt,
                    M1_Zm = recpacket.M1_Zm,
                    M1_Zt = recpacket.M1_Zt,
                    M2_ADDR = recpacket.M2_ADDR,
                    M2_Status = recpacket.M2_Status,
                    M2_Vxm = recpacket.M2_Vxm,
                    M2_Vxt = recpacket.M2_Vxt,
                    M2_Vym = recpacket.M2_Vym,
                    M2_Vyt = recpacket.M2_Vyt,
                    M2_Vzm = recpacket.M2_Vzm,
                    M2_Vzt = recpacket.M2_Vzt,
                    M2_Xm = recpacket.M2_Xm,
                    M2_Xt = recpacket.M2_Xt,
                    M2_Ym = recpacket.M2_Ym,
                    M2_Yt = recpacket.M2_Yt,
                    M2_Zm = recpacket.M2_Zm,
                    M2_Zt = recpacket.M2_Zt,
                    M3_ADDR = recpacket.M3_ADDR,
                    M3_Status = recpacket.M3_Status,
                    M3_Vxm = recpacket.M3_Vxm,
                    M3_Vxt = recpacket.M3_Vxt,
                    M3_Vym = recpacket.M3_Vym,
                    M3_Vyt = recpacket.M3_Vyt,
                    M3_Vzm = recpacket.M3_Vzm,
                    M3_Vzt = recpacket.M3_Vzt,
                    M3_Xm = recpacket.M3_Xm,
                    M3_Xt = recpacket.M3_Xt,
                    M3_Ym = recpacket.M3_Ym,
                    M3_Yt = recpacket.M3_Yt,
                    M3_Zm = recpacket.M3_Zm,
                    M3_Zt = recpacket.M3_Zt,
                    M4_ADDR = recpacket.M4_ADDR,
                    M4_Status = recpacket.M4_Status,
                    M4_Vxm = recpacket.M4_Vxm,
                    M4_Vxt = recpacket.M4_Vxt,
                    M4_Vym = recpacket.M4_Vym,
                    M4_Vyt = recpacket.M4_Vyt,
                    M4_Vzm = recpacket.M4_Vzm,
                    M4_Vzt = recpacket.M4_Vzt,
                    M4_Xm = recpacket.M4_Xm,
                    M4_Xt = recpacket.M4_Xt,
                    M4_Ym = recpacket.M4_Ym,
                    M4_Yt = recpacket.M4_Yt,
                    M4_Zm = recpacket.M4_Zm,
                    M4_Zt = recpacket.M4_Zt,
                    M5_ADDR = recpacket.M5_ADDR,
                    M5_Status = recpacket.M5_Status,
                    M5_Vxm = recpacket.M5_Vxm,
                    M5_Vxt = recpacket.M5_Vxt,
                    M5_Vym = recpacket.M5_Vym,
                    M5_Vyt = recpacket.M5_Vyt,
                    M5_Vzm = recpacket.M5_Vzm,
                    M5_Vzt = recpacket.M5_Vzt,
                    M5_Xm = recpacket.M5_Xm,
                    M5_Xt = recpacket.M5_Xt,
                    M5_Ym = recpacket.M5_Ym,
                    M5_Yt = recpacket.M5_Yt,
                    M5_Zm = recpacket.M5_Zm,
                    M5_Zt = recpacket.M5_Zt,
                    M6_ADDR = recpacket.M6_ADDR,
                    M6_Status = recpacket.M6_Status,
                    M6_Vxm = recpacket.M6_Vxm,
                    M6_Vxt = recpacket.M6_Vxt,
                    M6_Vym = recpacket.M6_Vym,
                    M6_Vyt = recpacket.M6_Vyt,
                    M6_Vzm = recpacket.M6_Vzm,
                    M6_Vzt = recpacket.M6_Vzt,
                    M6_Xm = recpacket.M6_Xm,
                    M6_Xt = recpacket.M6_Xt,
                    M6_Ym = recpacket.M6_Ym,
                    M6_Yt = recpacket.M6_Yt,
                    M6_Zm = recpacket.M6_Zm,
                    M6_Zt = recpacket.M6_Zt,
                    UpPower = recpacket.UpPower
                };
                await _hubContext.Clients.All.SendAsync("ReceiveData", recpacketToUi);
            }
            return Ok(recpacket);
        }
       
    }
    
}
