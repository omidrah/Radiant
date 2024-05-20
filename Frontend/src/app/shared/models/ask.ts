export class askmodel {
    // cmd: cmd;
    // m1tab: m1tab;
    testmode: string;
    datamode: string;
    couple:number;
    unit:string;
    pwr:number;
    att:number
    /******** */
    m1downdata: string;
    m1xm: number;
    m1ym:number;
    m1zm:number;
    m1status:number;
    m1selstatus:number
    m1counter:number
    m1common:number
    m1ontime:number
    m1linkled:number
    m1adm:number
    m1freq:number
    };

 export interface cmd {
    testmode: string;
    datamode: string;
    couple:number;
    unit:string;
    pwr:number;
    att:number
};
export interface m1tab {
    m1downdata: string;
    m1xm: number;
    m1ym:number;
    m1zm:number;
    m1status:number;
    m1selstatus:number
    m1counter:number
    m1common:number
    m1ontime:number
    m1linkled:number
    m1adm:number
    m1freq:number
};