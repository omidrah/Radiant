
typedef struct
{
	char header[3];
	u8 test_mode;
	u8 downlink_data_mode;
	u8 ATT_Value;
	u8 frequency;
	u8 rsvd1;
	s32 M1_Xm;
	s32 M1_Ym;
	s32 M1_Zm;
	u8 M1_status;
	u8 M1_ADM;
//	u8 rsvd2[2];
	u8 BIT_Enable;
	u8 CRC_Enable;
	s32 M2_Xm;
	s32 M2_Ym;
	s32 M2_Zm;
	u8 M2_status;
	u8 M2_ADM;
	u8 rsvd3[2];
	s32 M3_Xm;
	s32 M3_Ym;
	s32 M3_Zm;
	u8 M3_status;
	u8 M3_ADM;
	u8 rsvd4[2];
	s32 M4_Xm;
	s32 M4_Ym;
	s32 M4_Zm;
	u8 M4_status;
	u8 M4_ADM;
	u8 rsvd5[2];
	s32 M5_Xm;
	s32 M5_Ym;
	s32 M5_Zm;
	u8 M5_status;
	u8 M5_ADM;
	u8 rsvd6[2];
	s32 M6_Xm;
	s32 M6_Ym;
	s32 M6_Zm;
	u8 M6_status;
	u8 M6_ADM;
//	u8 rsvd[2];
	u16 checkSum;
	u8 rsvd7;
	char footer[3];
} rcv_field_t;







typedef struct{
	char head[4];
	u16 MissleAddress;
	u16 upPower;
	u16 Xt;
	u16 Yt;
	u16 Zt;
	u16 Xm;
	u16 Ym;
	u16 Zm;
	u16 Vxm;
	u16 Vym;
	u16 Vzm;
	u16 Vxt;
	u16 Vyt;
	u16 Vzt;
	u16 Ctrl;
	u16 ResetTime;
	u16 CRC16;
	u16 checkSum;
	char footer[4];
}snd_field_t;



