import { Component, OnInit } from '@angular/core';
import { Config } from 'src/app/models/config.model';

@Component({
  selector: 'app-config-app',
  templateUrl: './config-app.component.html',
  styles: [
  ]
})
export class ConfigAppComponent implements OnInit {
  configService: any;
  config: Config | undefined; //{ apiUrl: string }

  constructor() { }

  ngOnInit(): void {
  }

  showConfig() {
    this.configService.getConfig()
      .subscribe((data: Config) => this.config = {
        ...data  //apiUrl: (data as any).apiUrl
      });
    }

}
