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
	u8 downlink_data_mode;
	u8 crc;
	s32 M2_Xm;
	s32 M2_Ym;
	s32 M2_Zm;
	u8 M2_status;
	u8 M2_ADM;
	u8 downlink_data_mode;
	u8 rsvd3[1]; // cfar_coef 2000-10000 LSB (num && 0x00FF)
	s32 M3_Xm;
	s32 M3_Ym;
	s32 M3_Zm;
	u8 M3_status;
	u8 M3_ADM;
	u8 downlink_data_mode;
	u8 rsvd4[1]; // cfar_coef 2000-10000 MSB (num >> 8 yaaaa num%256)
	s32 M4_Xm;
	s32 M4_Ym;
	s32 M4_Zm;
	u8 M4_status;
	u8 M4_ADM;	
	u8 downlink_data_mode;
	u8 rsvd5[1]; //downlink_att 0-84 1-byte
	s32 M5_Xm;
	s32 M5_Ym;
	s32 M5_Zm;
	u8 M5_status;
	u8 M5_ADM;
	u8 downlink_data_mode;
	u8 rsvd6[1]; // uplink_gain 0-74 1-byte
	s32 M6_Xm;
	s32 M6_Ym;
	s32 M6_Zm;
	u8 M6_status;
	u8 M6_ADM;
	u8 downlink_data_mode;
	u8 rsvd7[1]; //self_tester_att 0-89 1-byte
	u16 checkSum;	
	u8 rsvd8[1];
	char footer[3];
} rcv_field_t;
