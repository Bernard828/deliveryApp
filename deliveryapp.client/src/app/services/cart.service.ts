import { Injectable, signal, computed } from '@angular/core';
import { MenuItemSearchDto } from '../models/menuItem.model';

export interface CartItem {
  menuItemId: number;
  name: string;
  price: number;
  quantity: number;
  imageUrl: string;
}
@Injectable({
  providedIn: 'root'
})
export class CartService {
  private items = signal<CartItem[]>([]);


  public totalItemsCount = computed(() =>
    this.items().reduce((sum, item) => sum + item.quantity, 0)
  );

  public subTotal = computed(() =>
    this.items().reduce((sum, item) =>
      sum + (item.price * item.quantity), 0)
  );

  public deliveryFee = signal<number>(4.99); //Flat delivery fee

  public totalPrice = computed(() =>
    this.subTotal() + this.deliveryFee()
  );

  getCart() {
    return this.items();
  }

  addItem(item: MenuItemSearchDto, quantity: number) {
    this.items.update(list => {
      const existing = list.find(i => i.menuItemId === item.menuItemId);

      if (existing) {
        return list.map(i =>
          i.menuItemId === item.menuItemId
            ? { ...i, quantity: i.quantity + quantity }
            : i
        );
      }

      return [
        ...list,
        {
          menuItemId: item.menuItemId,
          name: item.name,
          price: item.price,
          quantity,
          imageUrl: item.imageUrl
        }
      ];
    });
  }

  updateQty(menuItemId: number, qty: number) {
    this.items.update(list =>
      list.map(i =>
        i.menuItemId === menuItemId ? {
          ...i, quantity: qty
        } : i));
  }

  removeItem(menuItemId: number) {
    this.items.update(list =>
      list.filter(i => i.menuItemId !== menuItemId));
  }

  clear() {
    this.items.set([]);
  }
}
