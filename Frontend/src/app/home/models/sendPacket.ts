export class SendPacket {constructor(
  public header: string,
  public testmode: string,
  public couple: number,
  public att: number,
  public mfreq: number, //same for all tab
  public selftest: number,
  public m1xm: number,
  public m1ym: number,
  public m1zm: number,
  public m1status: string,
  public m1adm: string,
  public datamode1: string,
  public crc: number,
  public m2xm: number,
  public m2ym: number,
  public m2zm: number,
  public m2status: string,
  public m2adm: string,
  public datamode2: string,
  public cfar_coef: number, //rsvd3
  public m3xm: number,
  public m3ym: number,
  public m3zm: number,
  public m3status: string,
  public m3adm: string,
  public datamode3: string,
  public rsvd4: number, //0x00
  public m4xm: number,
  public m4ym: number,
  public m4zm: number,
  public m4status: string,
  public m4adm: string,
  public datamode4: string,
  public downlink_att: number,//rsvd5
  public m5xm: number,
  public m5ym: number,
  public m5zm: number,
  public m5status: string,
  public m5adm: string,
  public datamode5: string,
  public uplink_gain: number,//rsvd6
  public m6xm: number,
  public m6ym: number,
  public m6zm: number,
  public m6status: string,
  public m6adm: string,
  public datamode6: string,
  public self_tester_att: number, //rsvd7
  public checksum: number,
  public rsvd8: number,
  public footer: string
) {}   };
