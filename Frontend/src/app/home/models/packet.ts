import { ReceivePacket } from "./recievePacket";
import { SendPacket } from "./sendPacket";

export interface Packet {
  sPacket: SendPacket;
  rPacket: ReceivePacket;
}

// Define the Packet class with Proxy for dynamic getters and setters
export class PacketImpl implements Packet {
     sPacket: SendPacket;
     rPacket: ReceivePacket;

    constructor(sPacket: SendPacket, rPacket: ReceivePacket) {
        this.sPacket = sPacket;
        this.rPacket = rPacket;
        return new Proxy(this, {
          get(target, prop, receiver) {
              if (prop in target.sPacket) {
                  return (target.sPacket as any)[prop];
              }
              if (prop in target.rPacket) {
                  return (target.rPacket as any)[prop];
              }
              return Reflect.get(target, prop, receiver);
          },
          set(target, prop, value, receiver) {
              if (prop in target.sPacket) {
                  (target.sPacket as any)[prop] = value;
                  return true;
              }
              if (prop in target.rPacket) {
                  (target.rPacket as any)[prop] = value;
                  return true;
              }
              return Reflect.set(target, prop, value, receiver);
          }
      });
  }
    // Optional: Add specific methods if needed
}
