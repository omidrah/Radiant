
typedef struct
{
	char header[3];
	u8 test_mode;
	u8 couple;	
	u8 ATT_Value;
	u8 frequency;
	u8 selftest;
	s32 M1_Xm;
	s32 M1_Ym;
	s32 M1_Zm;
	u8 M1_status;
	u8 M1_ADM;
	u8 downlink_data_mode1;
	u8 crc;
	s32 M2_Xm;
	s32 M2_Ym;
	s32 M2_Zm;
	u8 M2_status;
	u8 M2_ADM;
	u8 downlink_data_mode2;
	u8 rsvd3[1]; // cfar_coef 0-255 LSB 
	s32 M3_Xm;
	s32 M3_Ym;
	s32 M3_Zm;
	u8 M3_status;
	u8 M3_ADM;
	u8 downlink_data_mode3;
	u8 rsvd4[1]; // 0x00
	s32 M4_Xm;
	s32 M4_Ym;
	s32 M4_Zm;
	u8 M4_status;
	u8 M4_ADM;	
	u8 downlink_data_mode4;
	u8 rsvd5[1]; //downlink_att 0-84 1-byte
	s32 M5_Xm;
	s32 M5_Ym;
	s32 M5_Zm;
	u8 M5_status;
	u8 M5_ADM;
	u8 downlink_data_mode5;
	u8 rsvd6[1]; // uplink_gain 0-74 1-byte
	s32 M6_Xm;
	s32 M6_Ym;
	s32 M6_Zm;
	u8 M6_status;
	u8 M6_ADM;
	u8 downlink_data_mode6;
	u8 rsvd7[1]; //self_tester_att 0-89 1-byte
	u16 checkSum;	
	u8 rsvd8[1];
	char footer[3];
} rcv_field_t;




pkt_snd.field. head[0] = 'R';
pkt_snd.field.head[1] = 'C';
pkt_snd.field.head[2] = 'V';
pkt_snd.field.head[3] = 'U';
pkt_snd.field.footer[0] = 'P';
pkt_snd.field.footer[1] = 'L';
pkt_snd.field.footer[2] = 'I';
pkt_snd.field.footer[3] = 'N';



typedef struct{
	char head[4];
	u16 upPower;
	
	u16 M1_ADDR;
	s16 M1_Xt;
	s16 M1_Yt;
	s16 M1_Zt;
	s16 M1_Xm;
	s16 M1_Ym;
	s16 M1_Zm;
	s16 M1_Vxm;
	s16 M1_Vym;
	s16 M1_Vzm;
	s16 M1_Vxt;
	s16 M1_Vyt;
	s16 M1_Vzt;
	u16 M1_Status;
	
	u16 M2_ADDR;
	s16 M2_Xt;
	s16 M2_Yt;
	s16 M2_Zt;
	s16 M2_Xm;
	s16 M2_Ym;
	s16 M2_Zm;
	s16 M2_Vxm;
	s16 M2_Vym;
	s16 M2_Vzm;
	s16 M2_Vxt;
	s16 M2_Vyt;
	s16 M2_Vzt;
	u16 M2_Status;
	
	u16 M3_ADDR;
	s16 M3_Xt;
	s16 M3_Yt;
	s16 M3_Zt;
	s16 M3_Xm;
	s16 M3_Ym;
	s16 M3_Zm;
	s16 M3_Vxm;
	s16 M3_Vym;
	s16 M3_Vzm;
	s16 M3_Vxt;
	s16 M3_Vyt;
	s16 M3_Vzt;
	u16 M3_Status;
	
	u16 M4_ADDR;
	s16 M4_Xt;
	s16 M4_Yt;
	s16 M4_Zt;
	s16 M4_Xm;
	s16 M4_Ym;
	s16 M4_Zm;
	s16 M4_Vxm;
	s16 M4_Vym;
	s16 M4_Vzm;
	s16 M4_Vxt;
	s16 M4_Vyt;
	s16 M4_Vzt;
	u16 M4_Status;
	
	u16 M5_ADDR;
	s16 M5_Xt;
	s16 M5_Yt;
	s16 M5_Zt;
	s16 M5_Xm;
	s16 M5_Ym;
	s16 M5_Zm;
	s16 M5_Vxm;
	s16 M5_Vym;
	s16 M5_Vzm;
	s16 M5_Vxt;
	s16 M5_Vyt;
	s16 M5_Vzt;
	u16 M5_Status;
	
	u16 M6_ADDR;
	s16 M6_Xt;
	s16 M6_Yt;
	s16 M6_Zt;
	s16 M6_Xm;
	s16 M6_Ym;
	s16 M6_Zm;
	s16 M6_Vxm;
	s16 M6_Vym;
	s16 M6_Vzm;
	s16 M6_Vxt;
	s16 M6_Vyt;
	s16 M6_Vzt;
	u16 M6_Status;
	u16 checkSum;
	char footer[4];
}snd_field_t;


	pkt_snd.field.M1_ADDR = 11;
	pkt_snd.field.M1_Status = 21;
	pkt_snd.field.M1_Xm = 51;
	pkt_snd.field.M1_Ym = 52;
	pkt_snd.field.M1_Zm = 53;
	pkt_snd.field.M1_Xt = 61;
	pkt_snd.field.M1_Yt = 62;
	pkt_snd.field.M1_Zt = 63;
	pkt_snd.field.M1_Vxm = 31;
	pkt_snd.field.M1_Vym= 32;
	pkt_snd.field.M1_Vzm= 33;
	pkt_snd.field.M1_Vxt = 41;
	pkt_snd.field.M1_Vyt= 42;
	pkt_snd.field.M1_Vzt= 43;

	pkt_snd.field.M2_ADDR = 12;
	pkt_snd.field.M2_Status = 22;
	pkt_snd.field.M2_Xm = 54;
	pkt_snd.field.M2_Ym = 55;
	pkt_snd.field.M2_Zm = 56;
	pkt_snd.field.M2_Xt = 64;
	pkt_snd.field.M2_Yt = 65;
	pkt_snd.field.M2_Zt = 66;
	pkt_snd.field.M2_Vzm= 34;
	pkt_snd.field.M2_Vym= 35;
	pkt_snd.field.M2_Vzm= 36;
	pkt_snd.field.M2_Vxt = 44;
	pkt_snd.field.M2_Vyt= 45;
	pkt_snd.field.M2_Vzt= 46;

	pkt_snd.field.M3_ADDR = 13;
	pkt_snd.field.M3_Status = 23;
	pkt_snd.field.M3_Xm = 57;
	pkt_snd.field.M3_Ym = 58;
	pkt_snd.field.M3_Zm = 59;
	pkt_snd.field.M3_Xt = 67;
	pkt_snd.field.M3_Yt = 68;
	pkt_snd.field.M3_Zt = 69;
	pkt_snd.field.M3_Vzm= 37;
	pkt_snd.field.M3_Vym= 38;
	pkt_snd.field.M3_Vzm= 39;
	pkt_snd.field.M3_Vxt = 47;
	pkt_snd.field.M3_Vyt= 48;
	pkt_snd.field.M3_Vzt= 49;

	pkt_snd.field.M4_ADDR = 14;
	pkt_snd.field.M4_Status = 24;
	pkt_snd.field.M4_Xm = 60;
	pkt_snd.field.M4_Ym = 61;
	pkt_snd.field.M4_Zm = 62;
	pkt_snd.field.M4_Xt = 70;
	pkt_snd.field.M4_Yt = 71;
	pkt_snd.field.M4_Zt = 72;
	pkt_snd.field.M4_Vzm= 40;
	pkt_snd.field.M4_Vym= 41;
	pkt_snd.field.M4_Vzm= 42;
	pkt_snd.field.M4_Vxt = 50;
	pkt_snd.field.M4_Vyt= 51;
	pkt_snd.field.M4_Vzt= 52;

	pkt_snd.field.M5_ADDR = 15;
	pkt_snd.field.M5_Status = 25;
	pkt_snd.field.M5_Xm = 63;
	pkt_snd.field.M5_Ym = 64;
	pkt_snd.field.M5_Zm = 65;
	pkt_snd.field.M5_Xt = 73;
	pkt_snd.field.M5_Yt = 74;
	pkt_snd.field.M5_Zt = 75;
	pkt_snd.field.M5_Vzm= 43;
	pkt_snd.field.M5_Vym= 44;
	pkt_snd.field.M5_Vzm= 45;
	pkt_snd.field.M5_Vxt = 53;
	pkt_snd.field.M5_Vyt= 54;
	pkt_snd.field.M5_Vzt= 55;

	pkt_snd.field.M6_ADDR = 16;
	pkt_snd.field.M6_Status = 26;
	pkt_snd.field.M6_Xm = 66;
	pkt_snd.field.M6_Ym = 67;
	pkt_snd.field.M6_Zm = 68;
	pkt_snd.field.M6_Xt = 76;
	pkt_snd.field.M6_Yt = 77;
	pkt_snd.field.M6_Zt = 78;
	pkt_snd.field.M6_Vzm= 46;
	pkt_snd.field.M6_Vym= 47;
	pkt_snd.field.M6_Vzm= 48;
	pkt_snd.field.M6_Vxt = 56;
	pkt_snd.field.M6_Vyt= 57;
	pkt_snd.field.M6_Vzt= 58;





