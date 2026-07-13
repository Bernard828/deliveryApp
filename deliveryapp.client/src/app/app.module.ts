import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RestaurantListComponent } from './component/restaurant-list/restaurant-list.component';
import { MenuComponent } from './component/menu/menu.component';
import { OrderTrackingComponent } from './component/order-tracking/order-tracking.component';
import { providePrimeNG } from 'primeng/config';
import Aura from '@primeuix/themes/aura';

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
  providers: [
    provideAnimationsAsync(),
    providePrimeNG({
      theme: {
        preset: Aura,
        options: {
          darkmodeselector: '[data-theme="dark"]',
          lightmodeselector: '[data-theme="light"]'
        }
      }
    })
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
