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
  { path: 'home.component', component: HomeComponent, title: 'Home' },
  { path: 'restaurant.component', component: RestaurantComponent, title: 'Restaurants' },
  { path: 'user.component', component: UserComponent, title: 'User' },
  { path: 'menu.component', component: MenuComponent, title: 'Menu' },
  { path: 'order.component', component: OrderComponent, title: 'Order' },
  { path: 'order-tracking.component', component: OrderTrackingComponent, title: 'Order Progress' },
  { path: 'restaurant-list.component', component: RestaurantListComponent, title: 'Restaurants' },
  { path: '', redirectTo: 'home.component' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
