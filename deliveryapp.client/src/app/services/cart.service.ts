import { Injectable, signal, computed } from '@angular/core';
import { CartItem } from '../models/cart-item-model';
import { BehaviorSubject } from 'rxjs';
import { OrderItemDto } from '../models/menuItem.model';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private cartItems = new BehaviorSubject<OrderItemDto[]>([]);
  cartItems$ = this.cartItems.asObservable();

  private activeRestaurantId = new BehaviorSubject<number | null>(null);
  activeRestaurantId$ = this.activeRestaurantId.asObservable();

  private cartItemSignal = signal<CartItem[]>([]); //Reactive State

  //public cartItems = this.cartItemSignal.asReadonly(); //signals for components to subscribe to

  public totalItemsCount = computed(() =>
    this.cartItemSignal().reduce((sum, item) => sum + item.quantity, 0)
  );

  public subtotal = computed(() =>
    this.cartItemSignal().reduce((sum, item) =>
      sum + (item.price * item.quantity), 0)
  );

  public deliveryFee = signal<number>(4.99); //Flat delivery fee
  public grandTotal = computed(() => this.subtotal() + this.deliveryFee());

  //One Restaurant at a time - if user tries to add item from different restaurant, clear cart first
  public currentRestaurantId = computed(() => {
    const items = this.cartItemSignal();
    return items.length > 0 ? items[0].restaurantId : null; //Assumes all items are from the same restaurant
  });

  //Add item to cart, clear items if starting order with different restaurant
  addToCartOld(newItem: Omit<CartItem, 'quantity'>): void {
    const currentItems = this.cartItemSignal();
    const activeRestaurantId = this.currentRestaurantId();

    //Rule Validation
    if (activeRestaurantId !== null && activeRestaurantId !== newItem.restaurantId) {
      const confirmClear = confirm("You have items from another restaurant in your cart. Do you want to clear your cart to add this item?");
      if (!confirmClear) return;
      this.clearCart();
    }

    const existingItemIndex = currentItems.findIndex(item => item.menuItemId === newItem.menuItemId);
    if (existingItemIndex > -1) {
      const updatedItems = [...currentItems];
      updatedItems[existingItemIndex] = {
        ...updatedItems[existingItemIndex],
        quantity: updatedItems[existingItemIndex].quantity + 1
      };
      this.cartItemSignal.set(updatedItems);
    } else {
      this.cartItemSignal.set([...currentItems, { ...newItem, quantity: 1 }]);
    }
  }

  addToCart(item: OrderItemDto, restaurantId: number) {
    if (this.activeRestaurantId.value!==null && this.activeRestaurantId.value !== restaurantId){
      if (confirm('Clear your cart from the previous restaurnat?')) {
        this.clearCart();
      } else return;
    }
    this.activeRestaurantId.next(restaurantId);
    const current = [...this.cartItems.value];
    const existing = current.find(i => i.menuItemId === item.menuItemId);

    if (existing) {
      existing.quantity += item.quantity;
    } else current.push(item);
    this.cartItems.next(current);
  }  

  //Removes item when at 0 quantity, otherwise decrements quantity
  removeFromCart(menuItemId: number): void {
    const currentItems = this.cartItemSignal();
    const existingItemIndex = currentItems.findIndex(item => item.menuItemId);

    if (existingItemIndex === -1) return;

    const updatedItems = [...currentItems];
    const targetItem = updatedItems[existingItemIndex];

    if (targetItem.quantity > 1) {
      updatedItems[existingItemIndex] = { ...targetItem, quantity: targetItem.quantity - 1 };
      this.cartItemSignal.set(updatedItems);
    } else {
      this.cartItemSignal.set(currentItems.filter(item => item.menuItemId !== menuItemId));
    }
  }

  getCartCount(): number {
    return this.cartItems.value.reduce((sum, item) => sum + item.quantity, 0);
  }

  getCartItems(): OrderItemDto[] {
    return this.cartItems.value;
  }

  getRestaurantId(): number | null {
    return this.activeRestaurantId.value;
  }

  clearCart(): void {
    this.cartItems.next([]);
    this.activeRestaurantId.next(null);
    this.cartItemSignal.set([]);
  }
}
