using System.Runtime.InteropServices;

namespace WebApplication5.Model
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class RecievePacket
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Head; 
        public ushort MissleAddress; // u16 MissleAddress
        public ushort UpPower; // u16 upPower
        public ushort Xt; // u16 Xt
        public ushort Yt; // u16 Yt
        public ushort Zt; // u16 Zt
        public ushort Xm; // u16 Xm
        public ushort Ym; // u16 Ym
        public ushort Zm; // u16 Zm
        public ushort Vxm; // u16 Vxm
        public ushort Vym; // u16 Vym
        public ushort Vzm; // u16 Vzm
        public ushort Vxt; // u16 Vxt
        public ushort Vyt; // u16 Vyt
        public ushort Vzt; // u16 Vzt
        public ushort Ctrl; // u16 Ctrl
        public ushort ResetTime; // u16 ResetTime
        public ushort CRC16; // u16 CRC16
        public ushort CheckSum; // u16 checkSum

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Footer; 
    }
}

