﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace WebApplication5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        // GET: HomeController
        public bool Index()
        {
            byte[] blockdata = new byte[91];

            byte[] header = new byte[3] { 0x43, 0x4D, 0x44 }; 
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


            byte[] reverse = new byte[10] {  0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            Array.Copy(reverse, 0, blockdata, 73, 10);

            byte[] checksum = new byte[2] { 0x00,0x00 };
            Array.Copy(checksum, 0, blockdata, 83, 2);

            byte[] footer = new byte[3] { 0x00, 0x00,0x00 };
            Array.Copy(checksum, 0, blockdata, 85, 3);

            var path = $"d:\\words.txt";
            SaveData(path, blockdata);
           
            return true;
        }
        protected bool SaveData(string FileName, byte[] Data)
        
        {
            BinaryWriter Writer = null;
            try
            {
                // Create a new stream to write to the file
                Writer = new BinaryWriter(System.IO.File.OpenWrite(FileName));

                // Writer raw data                
                Writer.Write(Data);
                Writer.Flush();
                Writer.Close();
            }
            catch
            {
                //...
                return false;
            }

            return true;
        }
    }
}
