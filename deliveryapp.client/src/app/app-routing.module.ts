import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './component/home/home.component';
import { RestaurantComponent } from './pages/restaurant/restaurant.component';
import { UserComponent } from './pages/user/user.component';
import { MenuComponent } from './pages/menu/menu.component';
import { OrderComponent } from './pages/order/order.component';
import { RestaurantListComponent } from './pages/restaurant-list/restaurant-list.component';
import { OrderTrackingComponent } from './pages/order-tracking/order-tracking.component';

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
