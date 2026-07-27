import { Component, ChangeDetectionStrategy, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';

// PrimeNG
import { DialogModule } from 'primeng/dialog';
import { ButtonModule } from 'primeng/button';
import { MenubarModule } from 'primeng/menubar';
import { InputNumberModule } from 'primeng/inputnumber'

//Services
import { CartService } from '../../../services/cart.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    DialogModule,
    ButtonModule,
    InputNumberModule,
    FormsModule
  ],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class HeaderComponent {

  private cart = inject(CartService);

  cartVisible = false;

  totalItems = this.cart.totalItems;
  totalPrice = this.cart.totalPrice;

  openCart() {
    this.cartVisible = true;
  }

  closeCart() {
    this.cartVisible = false;
  }

  get items() {
    return this.cart.getCart();
  }

  remove(id: number) {
    this.cart.removeItem(id);
  }

  updateQty(id: number, qty: number) {
    this.cart.updateQty(id, qty);
  }
}
