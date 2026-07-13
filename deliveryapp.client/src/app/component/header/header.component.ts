import { Component, ChangeDetectionStrategy } from '@angular/core';
import { RouterLink, RouterModule, RouterOutlet } from '@angular/router';
import { MenubarModule } from 'primeng/menubar';
@Component({
  selector: 'app-header',
  standalone:true,
  imports: [RouterModule, RouterOutlet],
  templateUrl: './header.component.html',
  changeDetection: ChangeDetectionStrategy.Eager,
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  items = [
    { label: 'Home', routerLink: '/' },
    { label: 'Restaurants', RouterLink: '/restaurant-list' }
  ]
}
