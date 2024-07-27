
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

