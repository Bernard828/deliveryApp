import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './component/home/home.component';
import { RestaurantComponent } from './component/restaurant/restaurant.component';
import { UserComponent } from './component/user/user.component';
import { MenuComponent } from './component/menu/menu.component';
import { OrderComponent } from './component/order/order.component';
import { RestaurantListComponent } from './component/restaurant-list/restaurant-list.component';
import { OrderTrackingComponent } from './component/order-tracking/order-tracking.component';

const routes: Routes = [
  { path: 'home', component: HomeComponent, title: 'Home' },
  { path: 'restaurant', component: RestaurantComponent, title: 'Restaurant' },
  { path: 'user', component: UserComponent, title: 'User' },
  { path: 'menu', component: MenuComponent, title: 'Menu' },
  { path: 'order', component: OrderComponent, title: 'Order' },
  { path: 'order-tracking', component: OrderTrackingComponent, title: 'Order Progress' },
  { path: 'restaurant-list', component: RestaurantListComponent, title: 'Restaurants' },
  { path: '', redirectTo: '/restaurant-list', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
