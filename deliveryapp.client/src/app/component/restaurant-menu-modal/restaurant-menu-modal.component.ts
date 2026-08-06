import {
  Component,
  Input,
  ChangeDetectionStrategy,
  inject,
  OnChanges
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { DialogModule } from 'primeng/dialog';
import { MenuItemDto } from '../../models/menuItem.model';
import { MenuCategoryComponent } from '../menu-category/menu-category.component';
import { CartService } from '../../../services/cart.service';

@Component({
  selector: 'app-restaurant-menu-modal',
  standalone: true,
  imports: [CommonModule,
    DialogModule,
    MenuCategoryComponent,
  ],
  templateUrl: './restaurant-menu-modal.component.html',
  styleUrls: ['./restaurant-menu-modal.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class RestaurantMenuModalComponent implements OnChanges {
  @Input() visible = false;
  @Input() restaurantName = '';
  @Input() menuItems: MenuItemDto[] = [];

  private cart = inject(CartService);

  grouped = {
    Apps: [] as MenuItemDto[],
    SoupSalad: [] as MenuItemDto[],
    Sandwiches: [] as MenuItemDto[],
    Entrees: [] as MenuItemDto[],
    Kids: [] as MenuItemDto[],
    Dessert: [] as MenuItemDto[]
  };

  ngOnChanges() {
    this.groupedMenuItems();
  }

  groupedMenuItems() {
    Object.keys(this.grouped).forEach(
      key => (this.grouped[key as keyof typeof this.grouped] = [])
    );
  }

  addToCart(event: { item: MenuItemDto; quantity: number }) {
    this.cart.addItem(event.item, event.quantity);
  }
}
