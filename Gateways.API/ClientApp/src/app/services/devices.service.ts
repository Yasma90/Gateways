import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { API_Url } from 'urlconfig';
import { Device } from '../models/device.model';

@Injectable({
  providedIn: 'root'
})
export class DevicesService {

  constructor(
    private http: HttpClient) { }

  formData: Device = new Device();
  readonly baseUrl = `${API_Url}/PeripheralDevices`;
  list: Device[];
  selectedGateway:number;

  postDevice(){
    return this.http.post(this.baseUrl,this.formData);
  }

  putDevice(){
    return this.http.put(`${this.baseUrl}/${this.formData.id}`,this.formData);
  }

  deleteDevice(id: number){
    return this.http.delete(`${this.baseUrl}/${id}`);
  }

  refreshList(gatewayId?:number){
    var url = `${this.baseUrl}`;
    if(gatewayId != null)
      url = `${this.baseUrl}?gatewayId=${gatewayId}`;
    this.http.get(url)
    .toPromise()
    .then(res=>this.list = res as Device[]);
  }

}
