import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RestaurantListComponent } from './component/restaurant-list/restaurant-list.component';
import { MenuComponent } from './component/menu/menu.component';
import { OrderTrackingComponent } from './component/order-tracking/order-tracking.component';

@NgModule({
  declarations: [
    RestaurantListComponent,
    MenuComponent,
    OrderTrackingComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule, AppComponent
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
