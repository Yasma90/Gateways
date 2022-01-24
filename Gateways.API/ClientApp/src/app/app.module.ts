import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule, ToastContainerModule } from 'ngx-toastr';
import { MatDialogModule } from '@angular/material/dialog';
import {MatRadioModule} from '@angular/material/radio';
import {MatIconModule} from '@angular/material/icon';
import {MatBadgeModule} from '@angular/material/badge';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { GatewayListComponent } from './components/gateway-list/gateway-list.component';
import { GatewayAddComponent } from './components/gateway-add/gateway-add.component';
import { GatewayFormComponent } from './components/gateway-form/gateway-form.component';
import { DeviceFormComponent } from './components/device-form/device-form.component';
import { DeviceListComponent } from './components/device-list/device-list.component';

@NgModule({
  declarations: [
    AppComponent,
    GatewayListComponent,
    GatewayFormComponent,
    GatewayAddComponent,
    DeviceFormComponent,
    DeviceListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    ToastContainerModule,
    MatDialogModule,
    MatRadioModule,
    MatIconModule,
    MatBadgeModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
