import { Device } from "./device.model";

export class Gateway {
  id:number = 0;
  name:string;
  ipAddress:string;
  serialNumber: string;
  peripheralDevices: Device[];
}
