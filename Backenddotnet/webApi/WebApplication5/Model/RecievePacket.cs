using System.Runtime.InteropServices;

namespace WebApplication5.Model
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class RecievePacket
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Head; 
        public ushort UpPower { get; set; } // u16 upPower
        public ushort M1_ADDR { get; set; }
        public int M1_Xt { get; set; }
        public int M1_Yt { get; set; }
        public int M1_Zt{ get; set; }
        public int M1_Xm{ get; set; }
        public int M1_Ym{ get; set; }
        public int M1_Zm { get; set; }
        public int M1_Vxm{ get; set; }
        public int M1_Vym{ get; set; }
        public int M1_Vzm{ get; set; }
        public int M1_Vxt{ get; set; }
        public int M1_Vyt{ get; set; }
        public int M1_Vzt { get; set; }
        public ushort M1_Status { get; set; }

        public ushort M2_ADDR { get; set; }
        public int M2_Xt{ get; set; }
        public int M2_Yt{ get; set; }
        public int M2_Zt{ get; set; }
        public int M2_Xm{ get; set; }
        public int M2_Ym{ get; set; }
        public int M2_Zm { get; set; }
        public int M2_Vxm{ get; set; }
        public int M2_Vym{ get; set; }
        public int M2_Vzm{ get; set; }
        public int M2_Vxt{ get; set; }
        public int M2_Vyt{ get; set; }
        public int M2_Vzt { get; set; }
        public ushort M2_Status { get; set; }

        public ushort M3_ADDR { get; set; }
        public int M3_Xt{ get; set; }
        public int M3_Yt{ get; set; }
        public int M3_Zt{ get; set; }
        public int M3_Xm{ get; set; }
        public int M3_Ym{ get; set; }
        public int M3_Zm { get; set; }
        public int M3_Vxm{ get; set; }
        public int M3_Vym{ get; set; }
        public int M3_Vzm{ get; set; }
        public int M3_Vxt{ get; set; }
        public int M3_Vyt{ get; set; }
        public int M3_Vzt { get; set; }
        public ushort M3_Status { get; set; }

        public ushort M4_ADDR { get; set; }
        public int M4_Xt{ get; set; }
        public int M4_Yt{ get; set; }
        public int M4_Zt{ get; set; }
        public int M4_Xm{ get; set; }
        public int M4_Ym{ get; set; }
        public int M4_Zm { get; set; }
        public int M4_Vxm{ get; set; }
        public int M4_Vym{ get; set; }
        public int M4_Vzm{ get; set; }
        public int M4_Vxt{ get; set; }
        public int M4_Vyt{ get; set; }
        public int M4_Vzt { get; set; }
        public ushort M4_Status { get; set; }

        public ushort M5_ADDR { get; set; }
        public int M5_Xt{ get; set; }
        public int M5_Yt{ get; set; }
        public int M5_Zt{ get; set; }
        public int M5_Xm{ get; set; }
        public int M5_Ym{ get; set; }
        public int M5_Zm { get; set; }
        public int M5_Vxm{ get; set; }
        public int M5_Vym{ get; set; }
        public int M5_Vzm{ get; set; }
        public int M5_Vxt{ get; set; }
        public int M5_Vyt{ get; set; }
        public int M5_Vzt { get; set; }
        public ushort M5_Status { get; set; }

        public ushort M6_ADDR { get; set; }
        public int M6_Xt{ get; set; }
        public int M6_Yt{ get; set; }
        public int M6_Zt{ get; set; }
        public int M6_Xm{ get; set; }
        public int M6_Ym{ get; set; }
        public int M6_Zm { get; set; }
        public int M6_Vxm{ get; set; }
        public int M6_Vym{ get; set; }
        public int M6_Vzm{ get; set; }
        public int M6_Vxt{ get; set; }
        public int M6_Vyt{ get; set; }
        public int M6_Vzt { get; set; }
        public ushort M6_Status { get; set; }
        public ushort CheckSum { get; set; } // u16 checkSum
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Footer;
    }
    public class RecievePacketVm
    {
       
        public ushort UpPower { get; set; } 
        public ushort M1_ADDR { get; set; }
        public int M1_Xt { get; set; }
        public int M1_Yt { get; set; }
        public int M1_Zt { get; set; }
        public int M1_Xm { get; set; }
        public int M1_Ym { get; set; }
        public int M1_Zm { get; set; }
        public int M1_Vxm { get; set; }
        public int M1_Vym { get; set; }
        public int M1_Vzm { get; set; }
        public int M1_Vxt { get; set; }
        public int M1_Vyt { get; set; }
        public int M1_Vzt { get; set; }
        public ushort M1_Status { get; set; }

        public ushort M2_ADDR { get; set; }
        public int M2_Xt { get; set; }
        public int M2_Yt { get; set; }
        public int M2_Zt { get; set; }
        public int M2_Xm { get; set; }
        public int M2_Ym { get; set; }
        public int M2_Zm { get; set; }
        public int M2_Vxm { get; set; }
        public int M2_Vym { get; set; }
        public int M2_Vzm { get; set; }
        public int M2_Vxt { get; set; }
        public int M2_Vyt { get; set; }
        public int M2_Vzt { get; set; }
        public ushort M2_Status { get; set; }

        public ushort M3_ADDR { get; set; }
        public int M3_Xt { get; set; }
        public int M3_Yt { get; set; }
        public int M3_Zt { get; set; }
        public int M3_Xm { get; set; }
        public int M3_Ym { get; set; }
        public int M3_Zm { get; set; }
        public int M3_Vxm { get; set; }
        public int M3_Vym { get; set; }
        public int M3_Vzm { get; set; }
        public int M3_Vxt { get; set; }
        public int M3_Vyt { get; set; }
        public int M3_Vzt { get; set; }
        public ushort M3_Status { get; set; }

        public ushort M4_ADDR { get; set; }
        public int M4_Xt { get; set; }
        public int M4_Yt { get; set; }
        public int M4_Zt { get; set; }
        public int M4_Xm { get; set; }
        public int M4_Ym { get; set; }
        public int M4_Zm { get; set; }
        public int M4_Vxm { get; set; }
        public int M4_Vym { get; set; }
        public int M4_Vzm { get; set; }
        public int M4_Vxt { get; set; }
        public int M4_Vyt { get; set; }
        public int M4_Vzt { get; set; }
        public ushort M4_Status { get; set; }

        public ushort M5_ADDR { get; set; }
        public int M5_Xt { get; set; }
        public int M5_Yt { get; set; }
        public int M5_Zt { get; set; }
        public int M5_Xm { get; set; }
        public int M5_Ym { get; set; }
        public int M5_Zm { get; set; }
        public int M5_Vxm { get; set; }
        public int M5_Vym { get; set; }
        public int M5_Vzm { get; set; }
        public int M5_Vxt { get; set; }
        public int M5_Vyt { get; set; }
        public int M5_Vzt { get; set; }
        public ushort M5_Status { get; set; }

        public ushort M6_ADDR { get; set; }
        public int M6_Xt { get; set; }
        public int M6_Yt { get; set; }
        public int M6_Zt { get; set; }
        public int M6_Xm { get; set; }
        public int M6_Ym { get; set; }
        public int M6_Zm { get; set; }
        public int M6_Vxm { get; set; }
        public int M6_Vym { get; set; }
        public int M6_Vzm { get; set; }
        public int M6_Vxt { get; set; }
        public int M6_Vyt { get; set; }
        public int M6_Vzt { get; set; }
        public ushort M6_Status { get; set; }
    }


}

