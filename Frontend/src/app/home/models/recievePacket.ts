export interface ReceivePacket {
  head: string;
  missleAddress: number;
  upPower: number;
  xt: number;
  yt: number;
  zt: number;
  xm: number;
  ym: number;
  zm: number;
  vxm: number;
  vym: number;
  vzm: number;
  vxt: number;
  vyt: number;
  vzt: number;
  ctrl: number;
  resetTime: number;
  crc16: number;
  checkSum: number;
  footer: string;
}
