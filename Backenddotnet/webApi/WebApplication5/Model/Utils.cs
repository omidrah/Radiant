using System.Text;

namespace WebApplication5.Model
{
    public static class Utils
    {
        public static async Task FileWriteAsync(string filepath,string message, bool append = false)
        {
            var namefile = DateTime.Now.ToString("yyyy-dd-M-HH-mm-ss")+".txt";          
            try
            {
                using (FileStream stream = new FileStream(Path.Combine(filepath, namefile), append ? FileMode.Append : FileMode.Create, FileAccess.Write, FileShare.None, 4096, true))
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
        /// <summary>
        /// convert hex string to byteArray
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static byte[] StringToByteArray(string hex)
        {
            int length = hex.Length;
            byte[] bytes = new byte[length / 2];

            for (int i = 0; i < length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }
        /// <summary>
        /// Convert hex string to byte array.
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static byte[] HexStringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
        /// <summary>
        /// convert byteArray to hexString
        /// </summary>
        /// <param name="ba"></param>
        /// <returns></returns>
        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        /// <summary>
        ///  To represent Int32,short,byte,byte[] as a Hexadecimal string in C#,
        ///  use the ToString() method and set the base as the ToString() method’s second parameter i.e. 16 for Hexadecimal.
        /// </summary>
        /// <param name="numb">input value in int,short,byte or byte[]</param>
        /// <returns></returns>
        public static string convertToHex(int numb)
        {
            return string.Join("", BitConverter.GetBytes(numb).Select(b => b.ToString("X2")));
        }       
        public static string convertToHex(UInt16 numb)
        {
            return string.Join("", BitConverter.GetBytes(numb).Select(b => b.ToString("X2")));
        }       
        public static string convertToHex(short numb)
        {
            return string.Join("", BitConverter.GetBytes(numb).Select(b => b.ToString("X2")));
        }     
        public static string convertToHex(byte[] numb)
        {
            return string.Join("", numb.Select(b => b.ToString("X2")));
        }       
        public static string convertToHex(byte numb)
        {
            return string.Join("0x", numb.ToString("X2"));
        }

        /// <summary>
        /// convert a binary string to an uint
        /// </summary>
        /// <param name="binaryString"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="FormatException"></exception>
        public static uint ConvertBinaryStringToUInt32(string binaryString)
        {
            if (binaryString is null)
            {
                throw new ArgumentNullException(nameof(binaryString));
            }

            if (binaryString.Length > 32)
            {
                throw new ArgumentOutOfRangeException(nameof(binaryString), binaryString.Length, "The specified binary string can not be longer than 32 characters.");
            }

            uint result = 0u;

            for (int i = 0; i < binaryString.Length; i++)
            {
                result <<= 1;

                char c = binaryString[i];

                if (c == '0')
                {
                }
                else if (c == '1')
                {
                    result |= 1u;
                }
                else
                {
                    throw new FormatException($"Character {i} of binary string \"{binaryString}\" is an invalid '{c}'. Can only be '0' or '1'.");
                }
            }

            return result;
        }

        //To convert from decimal to hex
        /* string hexValue = decValue.ToString("X");  OR   string.Format("{0:x}", intValue);*/
        //To convert from hex to decimal 
        /*int decValue = Convert.ToInt32(hexValue, 16);    OR  Convert.ToInt64(hexString, 16);*/
    }
}
