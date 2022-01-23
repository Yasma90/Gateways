import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

import { Gateway } from 'src/app/models/gateway.model';
import { GatewaysService } from 'src/app/services/gateways.service';

@Component({
  selector: 'app-gateway-form',
  templateUrl: './gateway-form.component.html',
  styles: [
  ]
})

export class GatewayFormComponent implements OnInit {

  constructor(public service:GatewaysService,
    private toastr:ToastrService) { }

  ngOnInit(): void {
  }

  onSubmit(form:NgForm){
    if(this.service.formData.id == 0)
      this.addRecord(form);
    else
      this.updateRecord(form);
  }

  addRecord(form : NgForm){
    this.service.postGateway().subscribe(
      res => {
        this.resetForm(form);
        this.service.refreshList();
        this.toastr.success('Submitted successfully', 'Gateway Resgister');
      },
      err => {console.log(err);}
    );
  }

  updateRecord(form : NgForm) {
    this.service.putGateway().subscribe(
      res => {
        this.resetForm(form);
        this.service.refreshList();
        this.toastr.info('Updated successfully', 'Gateway updated');
      },
      err => {console.log(err);}
    );
  }

  resetForm(form : NgForm){
    form.form.reset();
    this.service.formData=new Gateway();
  }

}
