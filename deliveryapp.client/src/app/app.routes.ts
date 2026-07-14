import { Routes } from '@angular/router';

export const appRoutes: Routes = [
  {
    path: 'home',
    loadComponent: () =>
      import('./component/home/home.component').then(m => m.HomeComponent)
  },
  {
    path: 'restaurants',
    loadComponent: () =>
      import('./component/restaurant-list/restaurant-list.component').then(m => m.RestaurantListComponent)
  },
  {
    path: 'menu',
    loadComponent: () =>
      import('./component/menu/menu.component').then(m => m.MenuComponent)
  },
  {
    path: 'order',
    loadComponent: () =>
      import('./component/order/order.component').then(m => m.OrderComponent)
  },
  {
    path: 'track',
    loadComponent: () =>
      import('./component/order-tracking/order-tracking.component').then(m => m.OrderTrackingComponent)
  },
  {
    path: 'user',
    loadComponent: () =>
      import('./component/user/user.component').then(m => m.UserComponent)
  },
  {
    path: '',
    loadComponent: () =>
      import('./component/home/home.component').then(m => m.HomeComponent)
  },
  {
    path: '**',
    redirectTo: ''
  }
];
