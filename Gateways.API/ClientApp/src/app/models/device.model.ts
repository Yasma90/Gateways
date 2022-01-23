//import * as internal from "stream";
import { Gateway } from "./gateway.model";

export class Device {
  id:number;
  vendor:string;
  status: number;
  gatewayId:number;
  gateway: Gateway;
}
