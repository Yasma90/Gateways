import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Device } from 'src/app/models/device.model';
import { DevicesService } from 'src/app/services/devices.service';

@Component({
  selector: 'app-device-form',
  templateUrl: './device-form.component.html',
  styles: [
  ]
})
export class DeviceFormComponent implements OnInit {

  gatewayId:number;

  constructor(
    public service:DevicesService,
    private toastr:ToastrService) { }

  ngOnInit(): void {
    this.gatewayId = this.service.selectedGateway
    if( this.gatewayId !=0){
      this.service.formData.gatewayId = this.gatewayId;
    }
  }

  onSubmit(form:NgForm){
    if(this.service.formData.id != 0)
      this.updateRecord(form);
    else
      this.addRecord(form);
  }

  addRecord(form : NgForm){
    this.service.postDevice().subscribe(
      res => {
        this.resetForm(form);
        this.service.refreshList(this.gatewayId);
        this.toastr.success('Submitted successfully', 'Device Resgister');
      },
      err => {console.log(err);}
    );
  }

  updateRecord(form : NgForm) {
    this.service.putDevice().subscribe(
      res => {
        this.resetForm(form);
        this.service.refreshList(this.gatewayId);
        this.toastr.info('Updated successfully', 'Device updated');
      },
      err => {console.log(err);}
    );
  }

  resetForm(form : NgForm){
    form.form.reset();
    this.service.formData=new Device();
    this.service.formData.gatewayId = this.gatewayId;
  }

}
