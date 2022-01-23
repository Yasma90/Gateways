import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

import { Gateway } from '../../models/gateway.model';
import { GatewaysService } from '../../services/gateways.service';

@Component({
  selector: 'app-gateway-list',
  templateUrl: './gateway-list.component.html',
  styles: [
  ]
})

export class GatewayListComponent implements OnInit {

  constructor(public service: GatewaysService,
    private toastr:ToastrService) { }

  ngOnInit(): void {
    this.service.refreshList();
  }

  populateForm(selectedRecord: Gateway){
    this.service.formData = Object.assign({}, selectedRecord);
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
