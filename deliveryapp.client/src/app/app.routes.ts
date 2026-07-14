import { Routes } from '@angular/router';

export const appRoutes: Routes = [
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
