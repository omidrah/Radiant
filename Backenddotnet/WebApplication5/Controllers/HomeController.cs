using Microsoft.AspNetCore.Http;
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
            byte[] header = new byte[3] { 0x43,0x4D,0x44 };

            Array.Copy(header, 0, blockdata, 0, 3);

            blockdata[3] = 0x00;
            blockdata[4] = 0x01;
            blockdata[5] = 0x00;
            blockdata[6] = 0x00;
            blockdata[7] = 0x01;

            byte[] M1_Xm = new byte[3] { 0x43, 0x4D, 0x24 };
            Array.Copy(M1_Xm, 0, blockdata, 8, 3);
            byte[] M1_Ym = new byte[3] { 0x33, 0x4f, 0x04 };
            Array.Copy(M1_Ym, 0, blockdata, 11, 3);
            byte[] M1_Zm = new byte[3] { 0x23, 0x4a, 0x14 };
            Array.Copy(M1_Zm, 0, blockdata, 14, 3);
            

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
