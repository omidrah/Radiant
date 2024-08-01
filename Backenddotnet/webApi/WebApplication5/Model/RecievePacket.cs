using System.Runtime.InteropServices;

namespace WebApplication5.Model
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class RecievePacket
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Head; 
        public ushort UpPower; // u16 upPower

        public ushort M1_ADDR;
        public int M1_Xt;
        public int M1_Yt;
        public int M1_Zt;
        public int M1_Xm;
        public int M1_Ym;
        public int M1_Zm;
        public int M1_Vxm;
        public int M1_Vym;
        public int M1_Vzm;
        public int M1_Vxt;
        public int M1_Vyt;
        public int M1_Vzt;
        public ushort M1_Status;

        public ushort M2_ADDR;
        public int M2_Xt;
        public int M2_Yt;
        public int M2_Zt;
        public int M2_Xm;
        public int M2_Ym;
        public int M2_Zm;
        public int M2_Vxm;
        public int M2_Vym;
        public int M2_Vzm;
        public int M2_Vxt;
        public int M2_Vyt;
        public int M2_Vzt;
        public ushort M2_Status;

        public ushort M3_ADDR;
        public int M3_Xt;
        public int M3_Yt;
        public int M3_Zt;
        public int M3_Xm;
        public int M3_Ym;
        public int M3_Zm;
        public int M3_Vxm;
        public int M3_Vym;
        public int M3_Vzm;
        public int M3_Vxt;
        public int M3_Vyt;
        public int M3_Vzt;
        public ushort M3_Status;

        public ushort M4_ADDR;
        public int M4_Xt;
        public int M4_Yt;
        public int M4_Zt;
        public int M4_Xm;
        public int M4_Ym;
        public int M4_Zm;
        public int M4_Vxm;
        public int M4_Vym;
        public int M4_Vzm;
        public int M4_Vxt;
        public int M4_Vyt;
        public int M4_Vzt;
        public ushort M4_Status;

        public ushort M5_ADDR;
        public int M5_Xt;
        public int M5_Yt;
        public int M5_Zt;
        public int M5_Xm;
        public int M5_Ym;
        public int M5_Zm;
        public int M5_Vxm;
        public int M5_Vym;
        public int M5_Vzm;
        public int M5_Vxt;
        public int M5_Vyt;
        public int M5_Vzt;
        public ushort M5_Status;

        public ushort M6_ADDR;
        public int M6_Xt;
        public int M6_Yt;
        public int M6_Zt;
        public int M6_Xm;
        public int M6_Ym;
        public int M6_Zm;
        public int M6_Vxm;
        public int M6_Vym;
        public int M6_Vzm;
        public int M6_Vxt;
        public int M6_Vyt;
        public int M6_Vzt;
        public ushort M6_Status;
        public ushort CheckSum; // u16 checkSum
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Footer; 
    }
}

