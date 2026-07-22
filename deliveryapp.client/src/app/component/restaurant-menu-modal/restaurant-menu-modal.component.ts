import {
  Component,
  Input,
  ChangeDetectionStrategy,
  inject,
  OnChanges
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { DialogModule } from 'primeng/dialog';
import { MenuItemSearchDto } from '../../models/menuItem.model';
import { MenuCategoryComponent } from '../menu-category/menu-category.component';
import { CartService } from '../../services/cart.service';

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
  @Input() menuItems: MenuItemSearchDto[] = [];

  private cart = inject(CartService);

  grouped = {
    Apps: [] as MenuItemSearchDto[],
    SoupSalad: [] as MenuItemSearchDto[],
    Sandwiches: [] as MenuItemSearchDto[],
    Entrees: [] as MenuItemSearchDto[],
    Kids: [] as MenuItemSearchDto[],
    Dessert: [] as MenuItemSearchDto[]
  };

  ngOnChanges() {
    this.groupedMenuItems();
  }

  groupedMenuItems() {
    Object.keys(this.grouped).forEach(
      key => (this.grouped[key as keyof typeof this.grouped] = [])
    );

    // for (const item of this.menuItems) {
    //   this.grouped[item.category].push(item);
    // }
  }

  addToCart(event: { item: MenuItemSearchDto; quantity: number }) {
    this.cart.addItem(event.item, event.quantity);
  }
}
