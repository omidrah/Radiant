using System.Runtime.InteropServices;
using System.Text;
using WebApplication5.Services;

namespace WebApplication5.Model
{
    public static class Utils
    {
        
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
        /// <summary>
        /// Help Methood To Read byte Array and For Every byte Show binary code ,Hex code ,Decimal code ,Ascii Character.
        /// /// </summary>
        /// <param name="buffer">byte array recieved</param>
        /// <param name="asciiBuilder">all byte array convert to character Ascii and concatination and store to asciiBuilder. for zere or \0 put 0 to asciiBuilder String</param>
        /// <param name="bytesReceived">Lentgh of byte array recieved</param>
        public static void showByteArray(byte[] buffer, StringBuilder asciiBuilder, int bytesReceived)
        {
            Console.WriteLine("Data received from server byte one byte:");

            /*0-108 is echo of send byte from client*/
            for (int i = 108; i < bytesReceived; i++)
            {
                Console.WriteLine($"******************************************");
                Console.WriteLine($"index: {i}");

                byte binaryElement = buffer[i];

                // Convert the byte to its binary representation
                string binaryRepresentation = Convert.ToString(buffer[i], 2).PadLeft(8, '0');

                // Convert the byte to its hexadecimal representation
                string hexRepresentation = buffer[i].ToString("X2");

                // Convert the byte to its ASCII character representation
                char asciiCharacter = (char)buffer[i];

                // Print all representations
                Console.WriteLine($"Decimal: {binaryElement}");
                Console.WriteLine($"Binary: {binaryRepresentation}");
                Console.WriteLine($"Hexadecimal: {hexRepresentation}");
                Console.WriteLine($"ASCII Character: {asciiCharacter}");
                Console.WriteLine($"******************************************");

                if (asciiCharacter == '\0')
                    asciiBuilder.Append(binaryElement.ToString());
                else
                    asciiBuilder.Append(asciiCharacter);
            }
            Console.WriteLine("Data received from server Ascii:");
            string asciiString = asciiBuilder.ToString();
            Console.WriteLine(asciiString);
        }

        public static RecievePacket ByteArrayToDataPacket(byte[] data)
        {
            // Create a pointer to the byte array
            GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            try
            {
                IntPtr ptr = handle.AddrOfPinnedObject();
                // Convert pointer to DataPacket object
                return (RecievePacket)Marshal.PtrToStructure(ptr, typeof(RecievePacket));
            }
            finally
            {
                // Free the pinned handle
                handle.Free();
            }
        }
                /// <summary>
        /// Parse the byte array into the RecievePacke
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static RecievePacket BufferToRecievePacket(byte[] data)
        {
            RecievePacket packet = new();
            int offset = 0;

            packet.Head = new char[4];
            for (int i = 0; i < 4; i++)
            {
                packet.Head[i] = (char)data[offset++];
            }                     

            packet.UpPower = BitConverter.ToUInt16(data, offset);
            offset += 2;
            /************************/
            packet.M1_ADDR = BitConverter.ToUInt16(data, offset);
            offset += 2;

            packet.M1_Xt = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M1_Yt = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M1_Zt = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M1_Xm = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M1_Ym = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M1_Zm = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M1_Vxm = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M1_Vym = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M1_Vzm = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M1_Vxt = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M1_Vyt = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M1_Vzt = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M1_Status = BitConverter.ToUInt16(data, offset);
            offset += 2;
            /************************/
            packet.M2_ADDR = BitConverter.ToUInt16(data, offset);
            offset += 2;

            packet.M2_Xt = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M2_Yt = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M2_Zt = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M2_Xm = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M2_Ym = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M2_Zm = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M2_Vxm = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M2_Vym = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M2_Vzm = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M2_Vxt = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M2_Vyt = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M2_Vzt = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M2_Status = BitConverter.ToUInt16(data, offset);
            offset += 2;           
            /************************/
            packet.M3_ADDR = BitConverter.ToUInt16(data, offset);
            offset += 2;

            packet.M3_Xt = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M3_Yt = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M3_Zt = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M3_Xm = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M3_Ym = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M3_Zm = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M3_Vxm = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M3_Vym = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M3_Vzm = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M3_Vxt = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M3_Vyt = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M3_Vzt = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M3_Status = BitConverter.ToUInt16(data, offset);
            offset += 2;

            /************************/
            packet.M4_ADDR = BitConverter.ToUInt16(data, offset);
            offset += 2;

            packet.M4_Xt = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M4_Yt = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M4_Zt = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M4_Xm = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M4_Ym = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M4_Zm = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M4_Vxm = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M4_Vym = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M4_Vzm = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M4_Vxt = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M4_Vyt = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M4_Vzt = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M4_Status = BitConverter.ToUInt16(data, offset);
            offset += 2;
            
            /************************/
            packet.M5_ADDR = BitConverter.ToUInt16(data, offset);
            offset += 2;

            packet.M5_Xt = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M5_Yt = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M5_Zt = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M5_Xm = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M5_Ym = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M5_Zm = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M5_Vxm = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M5_Vym = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M5_Vzm = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M5_Vxt = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M5_Vyt = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M5_Vzt = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M5_Status = BitConverter.ToUInt16(data, offset);
            offset += 2;          
            /************************/
            packet.M6_ADDR = BitConverter.ToUInt16(data, offset);
            offset += 2;

            packet.M6_Xt = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M6_Yt = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M6_Zt = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M6_Xm = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M6_Ym = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M6_Zm = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M6_Vxm = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M6_Vym = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M6_Vzm = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M6_Vxt = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M6_Vyt = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M6_Vzt = BitConverter.ToInt32(data, offset);
            offset += 4;

            packet.M6_Status = BitConverter.ToUInt16(data, offset);
            offset += 2;

            packet.CheckSum = BitConverter.ToUInt16(data, offset);
            offset += 2;
            packet.Footer = new char[4];
            for (int i = 0; i < 4; i++)
            {
                packet.Footer[i] = (char)data[offset++];
            }
            if (packet != null)
            {
                LogRecievePacket(packet);
                switch (packet.Head.ToString())
                {
                    case "CMD":
                        Console.WriteLine("echo from server");
                        break;
                    case "RCUV":
                        Console.WriteLine("response from server");
                        break;
                    default:
                        break;
                }
            }
            return packet;
        }
        /// <summary>
        /// Convert RecievePacket to byte array here
        /// </summary>
        /// <param name="packet"></param>
        /// <returns></returns>
        public static byte[] RecievePacketToByteArray(RecievePacket packet)
        {
            
            // This is just a sample implementation. You need to adjust based on your RecievePacket structure.
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(packet.Head);
                    bw.Write(packet.UpPower);
                    
                    bw.Write(packet.M1_ADDR);
                    bw.Write(packet.M1_Xm);
                    bw.Write(packet.M1_Ym);
                    bw.Write(packet.M1_Zm);
                    bw.Write(packet.M1_Vxm);
                    bw.Write(packet.M1_Vym);
                    bw.Write(packet.M1_Vzm);
                    bw.Write(packet.M1_Vxt);
                    bw.Write(packet.M1_Vyt);
                    bw.Write(packet.M1_Vzt);
                    
                    bw.Write(packet.CheckSum);
                    bw.Write(packet.Footer);
                }
                return ms.ToArray();
            }
        }
        /// <summary>
        /// show receive packet in console
        /// </summary>
        /// <param name="packet"></param>
        public static void LogRecievePacket(RecievePacket packet)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine($"*******************************************************************");
            Console.WriteLine($"*                 Receive Packet                                  *");
            Console.WriteLine($"*******************************************************************");
            Console.WriteLine($"Header: {new string(packet.Head)}  |  Up Power: {packet.UpPower}  ");            
            Console.WriteLine($"*******************************************************************");
            Console.WriteLine($"     M1  |  M2   |  M3  |  M4  |   M5    |  M6  ");
            Console.WriteLine($"*******************************************************************");
            Console.WriteLine($"Xt:  {packet.M1_Xt }  |  {packet.M2_Xt }   |  {packet.M3_Xt }  |  {packet.M4_Xt }   |  {packet.M5_Xt }  |  {packet.M6_Xt }");
            Console.WriteLine($"Yt:  {packet.M1_Yt }  |  {packet.M2_Yt }   |  {packet.M3_Yt }  |  {packet.M4_Yt }   |  {packet.M5_Yt }  |  {packet.M6_Yt }");
            Console.WriteLine($"Zt:  {packet.M1_Zt }  |  {packet.M2_Zt }   |  {packet.M3_Zt }  |  {packet.M4_Zt }   |  {packet.M5_Zt }  |  {packet.M6_Zt }");
            Console.WriteLine($"Xm:  {packet.M1_Xm }  |  {packet.M2_Xm }   |  {packet.M3_Xm }  |  {packet.M4_Xm }   |  {packet.M5_Xm }  |  {packet.M6_Xm }");
            Console.WriteLine($"Ym:  {packet.M1_Ym }  |  {packet.M2_Ym }   |  {packet.M3_Ym }  |  {packet.M4_Ym }   |  {packet.M5_Ym }  |  {packet.M6_Ym }");
            Console.WriteLine($"Zm:  {packet.M1_Zm }  |  {packet.M2_Zm }   |  {packet.M3_Zm }  |  {packet.M4_Zm }   |  {packet.M5_Zm }  |  {packet.M6_Zm }");
            Console.WriteLine($"Vxm: {packet.M1_Vxm}  |  {packet.M2_Vxm}   |  {packet.M3_Vxm}  |  {packet.M4_Vxm}   |  {packet.M5_Vxm}  |  {packet.M6_Vxm}");
            Console.WriteLine($"Vym: {packet.M1_Vym}  |  {packet.M2_Vym}   |  {packet.M3_Vym}  |  {packet.M4_Vym}   |  {packet.M5_Vym}  |  {packet.M6_Vym}");
            Console.WriteLine($"Vzm: {packet.M1_Vzm}  |  {packet.M2_Vzm}   |  {packet.M3_Vzm}  |  {packet.M4_Vzm}   |  {packet.M5_Vzm}  |  {packet.M6_Vzm}");
            Console.WriteLine($"Vxt: {packet.M1_Vxt}  |  {packet.M2_Vxt}   |  {packet.M3_Vxt}  |  {packet.M4_Vxt}   |  {packet.M5_Vxt}  |  {packet.M6_Vxt}");
            Console.WriteLine($"Vyt: {packet.M1_Vyt}  |  {packet.M2_Vyt}   |  {packet.M3_Vyt}  |  {packet.M4_Vyt}   |  {packet.M5_Vyt}  |  {packet.M6_Vyt}");
            Console.WriteLine($"Vzt: {packet.M1_Vzt}  |  {packet.M2_Vzt}   |  {packet.M3_Vzt}  |  {packet.M4_Vzt}   |  {packet.M5_Vzt}  |  {packet.M6_Vzt}");
            Console.WriteLine($"*******************************************************************");
            Console.WriteLine($"Checksum: {packet.CheckSum}  | Footer: {new string(packet.Footer)}");
            Console.WriteLine($"*******************************************************************");
            Console.WriteLine($"*                 Receive Packet Finish                           *");
            Console.WriteLine($"*******************************************************************");
        }

        
    }
}
