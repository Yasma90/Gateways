import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { MatDialog } from '@angular/material/dialog';


import { Gateway } from '../../models/gateway.model';
import { GatewaysService } from '../../services/gateways.service';
import { DeviceListComponent } from '../device-list/device-list.component';
import { Device } from 'src/app/models/device.model';

@Component({
  selector: 'app-gateway-list',
  templateUrl: './gateway-list.component.html',
  styles: [
  ]
})

export class GatewayListComponent implements OnInit {

  constructor(public service: GatewaysService,
    private toastr:ToastrService,
    private dialog: MatDialog) { }

  ngOnInit(): void {
    this.service.refreshList();
  }

  populateForm(selectedRecord: Gateway){
    this.service.formData = Object.assign({}, selectedRecord);
  }

  infoDevices(id: number){
    console.log("Id gateway: ",id)
    this.dialog.open(DeviceListComponent, {
      data: id
    })
    .afterClosed().subscribe(() => {
      this.service.refreshList();
    });
  }

  onDelete(id: number){
    if(confirm('Are you sure to delete this record?')){
      this.service.deleteGateway(id)
      .subscribe(
        res => {
          this.service.refreshList();
          this.toastr.error("Deleted successfully.",'Gateway Register');
        },
        err => {console.log(err)}
      )
    }

  }


}
