import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule, ToastContainerModule } from 'ngx-toastr';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { GatewayListComponent } from './components/gateway-list/gateway-list.component';
import { GatewayAddComponent } from './components/gateway-add/gateway-add.component';
import { GatewayFormComponent } from './components/gateway-form/gateway-form.component';
import { DeviceFormComponent } from './components/device-form/device-form.component';

@NgModule({
  declarations: [
    AppComponent,
    GatewayListComponent,
    GatewayFormComponent,
    GatewayAddComponent,
    DeviceFormComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    ToastContainerModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
