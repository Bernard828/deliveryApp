import {
  Component,
  Input,
  Output,
  EventEmitter,
  ChangeDetectionStrategy
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { MenuItemDto } from '../../models/menuItem.model';
import { ButtonModule } from 'primeng/button';
import { InputNumberModule } from 'primeng/inputnumber';

@Component({
  selector: 'app-menu-item-card',
  standalone: true,
  imports:[CommonModule,ButtonModule,InputNumberModule],
  templateUrl: './menu-item-card.component.html',
  styleUrls: ['./menu-item-card.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class MenuItemCardComponent {
  @Input() item!: MenuItemDto;
  @Output() addToCart = new EventEmitter<{ item: MenuItemDto; quantity: number }>();

  quantity = 1;

  add() {
    this.addToCart.emit({ item: this.item, quantity: this.quantity });
  }
}
