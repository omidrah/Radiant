using System.Globalization;
using System.Text;

namespace WebApplication5.Model
{
    public class AskResult
    {
        public AskResult()
        {
            for (int i = 0; i < 2; i++) {
                rsvd2[i] = 0;
                rsvd3[i] = 0;
                rsvd4[i] = 0;
                rsvd5[i] = 0;
                rsvd6[i] = 0;
            }
        }
        public char[] header { get; set; } = { 'C', 'M', 'D' };
        public byte testmode { get; set; }
        public byte datamode { get; set; }
        public byte att { get; set; }
        public byte mfreq { get; set; }
        public byte rsvd1 { get; set; } = 0;

        public int m1xm { get; set; }
        public int m1ym { get; set; }
        public int m1zm { get; set; }
        public byte m1status { get; set; }
        public byte m1adm { get; set; }
        public byte[] rsvd2 { get; set; }=new byte[2];

        public int m2xm { get; set; }
        public int m2ym { get; set; }
        public int m2zm { get; set; }
        public byte m2status { get; set; }
        public byte m2adm { get; set; }
        public byte[] rsvd3 { get; set; } = new byte[2];

        public int m3xm { get; set; }
        public int m3ym { get; set; }
        public int m3zm { get; set; }
        public byte m3status { get; set; }
        public byte m3adm { get; set; }
        public byte[] rsvd4 { get; set; } = new byte[2];

        public int m4xm { get; set; }
        public int m4ym { get; set; }
        public int m4zm { get; set; }
        public byte m4status { get; set; }
        public byte m4adm { get; set; }
        public byte[] rsvd5 { get; set; } = new byte[2];

        public int m5xm { get; set; }
        public int m5ym { get; set; }
        public int m5zm { get; set; }
        public byte m5status { get; set; }
        public byte m5adm { get; set; }
        public byte[] rsvd6 { get; set; } = new byte[2];

        public int m6xm { get; set; }
        public int m6ym { get; set; }
        public int m6zm { get; set; }
        public byte m6status { get; set; }
        public byte m6adm { get; set; }
        public short checksum { get; set; } = 0;

        public byte rsvd7 { get; set; } = 0;
        //public byte[] reverse { get; set; } = new byte[10];
        public char[] footer { get; set; } = { 'E', 'N', 'D' };
        public override string ToString()
        {
            
            string fullHead = string.Empty;
            for (int i = 0; i < header.Length; i++)
            {
                fullHead += header[i].ToString();
            }
            string fullfoot = string.Empty;
            for (int i = 0; i < footer.Length; i++)
            {
                fullfoot += footer[i].ToString();
            }
            string r2 = string.Empty;
            foreach (byte b in rsvd2)
            {                
                r2 += b.ToString();
            }
            string r3 = string.Empty;
            foreach (byte b in rsvd3)
            {
                r3 += b.ToString();
            }
            string r4 = string.Empty;
            foreach (byte b in rsvd4)
            {
                r4 += b.ToString();
            }
            string r5 = string.Empty;
            foreach (byte b in rsvd5)
            {
                r5 += b.ToString();
            }
            string r6 = string.Empty;
            foreach (byte b in rsvd6)
            {
                r6 += b.ToString();
            }
            return $"{fullHead}{testmode}{datamode}{att}{mfreq}{rsvd1}" +
                $"{m1xm}{m1ym}{m1zm}{m1status}{m1adm}" +                
                $"{r2}" +
                $"{m2xm}{m2ym}{m2zm}{m2status}{m2adm}" +
                $"{r3}" +
                $"{m3xm}{m3ym}{m3zm}{m3status}{m3adm}" +
                $"{r4}" +
                $"{m4xm}{m4ym}{m4zm}{m4status}{m4adm}" +
                $"{r5}" +
                $"{m5xm}{m5ym}{m5zm}{m5status}{m5adm}" +
                $"{r6}" +
                $"{m6xm}{m6ym}{m6zm}{m6status}{m6adm}" +
                $"{checksum}{rsvd7}{fullfoot}";
        }
        public byte[] toByteArray()
        {
            byte[] barr = Encoding.ASCII.GetBytes(ToString());
            if (barr.Length > 106) { 
                Array.Resize(ref barr, 106);
            }
            else if(barr.Length < 106)
            {
                byte[] paddarr = new byte[106];
                barr.CopyTo(paddarr, 0);
                barr = paddarr;
            }
            return barr;    
        }
        /**/
        public int calculateChecksum()
        {
            return testmode + datamode + att + mfreq +
                 m1xm + m1ym + m1zm + m1status + m1adm +
                 m2xm + m2ym + m2zm + m2status + m2adm +
                 m3xm + m3ym + m3zm + m3status + m3adm +
                 m4xm + m4ym + m4zm + m4status + m4adm +
                 m5xm + m5ym + m5zm + m5status + m5adm +
                 m6xm + m6ym + m6zm + m6status + m6adm;
                 
        }
    }
}
