import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RestaurantListComponentComponent } from './restaurant-list-component/restaurant-list-component.component';
import { MenuComponentComponent } from './menu-component/menu-component.component';
import { OrderTrackingComponentComponent } from './order-tracking-component/order-tracking-component.component';

@NgModule({
  declarations: [
    AppComponent,
    RestaurantListComponentComponent,
    MenuComponentComponent,
    OrderTrackingComponentComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
