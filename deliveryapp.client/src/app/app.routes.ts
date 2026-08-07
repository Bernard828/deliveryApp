import { Routes } from '@angular/router';
import { AdminRestaurantListComponent } from './component/admin-restaurant-list/admin-restaurant-list.component';
import { AdminRestaurantDetailComponent } from './component/admin-restaurant-detail/admin-restaurant-detail.component';

export const appRoutes: Routes = [
  // {
  //   path: 'admin',
  //   children: [
  //     { path: 'resaturants', component: AdminRestaurantListComponent },
  //     { path: 'restaurants/:id', component: AdminRestaurantDetailComponent }
  //   ]
  // }
  {
    path: 'admin-dashboard',
    loadComponent: () =>
      import('./pages/admin-dashboard/admin-dashboard.component').then(m => m.AdminDashboardComponent)
  },
  {
    path: 'admin-restaurants',
    loadComponent: () =>
      import('./component/admin-restaurant-list/admin-restaurant-list.component').then(m => m.AdminRestaurantListComponent)
  },
  {
    path: 'admin-restaurant',
    loadComponent: () =>
      import('./component/admin-restaurant-detail/admin-restaurant-detail.component').then(m => m.AdminRestaurantDetailComponent)
  },
  {
    path: 'home',
    loadComponent: () =>
      import('./pages/home/home.component').then(m => m.HomeComponent)
  },
  {
    path: 'restaurants',
    loadComponent: () =>
      import('./pages/restaurant-list/restaurant-list.component').then(m => m.RestaurantListComponent)
  },
  {
    path: 'restaurant',
    loadComponent: () =>
      import('./component/restaurant/restaurant.component').then(m => m.RestaurantComponent)
  },
  {
    path: 'menu',
    loadComponent: () =>
      import('./pages/menu/menu.component').then(m => m.MenuComponent)
  },
  {
    path: 'order',
    loadComponent: () =>
      import('./pages/order/order.component').then(m => m.OrderComponent)
  },
  {
    path: 'track',
    loadComponent: () =>
      import('./pages/order-tracking/order-tracking.component').then(m => m.OrderTrackingComponent)
  },
  {
    path: 'user',
    loadComponent: () =>
      import('./pages/user/user.component').then(m => m.UserComponent)
  },
  {
    path: '',
    loadComponent: () =>
      import('./pages/home/home.component').then(m => m.HomeComponent)
  },
  {
    path: '**',
    redirectTo: ''
  }
];
