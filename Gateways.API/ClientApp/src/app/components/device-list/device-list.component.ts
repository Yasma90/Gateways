import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Device } from 'src/app/models/device.model';
import { DevicesService } from 'src/app/services/devices.service';
import { GatewaysService } from 'src/app/services/gateways.service';

@Component({
  selector: 'app-device-list',
  templateUrl: './device-list.component.html',
  styles: [
  ]
})
export class DeviceListComponent implements OnInit {
  devices: Device[] = [];

  constructor(public service: DevicesService,
    public gatewayService: GatewaysService,
    @Inject(MAT_DIALOG_DATA) public gatewayId: number,
    public dialogRef: MatDialogRef<DeviceListComponent>) { }

  ngOnInit(): void {
    if (this.gatewayId){
      this.service.refreshList(this.gatewayId);
      this.service.selectedGateway = this.gatewayId;
    }
  }

  getDevicesbyId() {
    this.gatewayService.getGateway(this.gatewayId)
      .subscribe(res => this.devices = res.peripheralDevices
      );
  }

  populateForm(selectedRecord: Device){
    this.service.formData = Object.assign({}, selectedRecord);
  }

  onDelete(id: number):void{
    this.service.deleteDevice(id)
    .subscribe(()=>{this.dialogRef.close();});
   }

}
