import {
  Component,
  Input,
  Output,
  ChangeDetectionStrategy,
  EventEmitter,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { MenuItemDto } from '../../models/menuItem.model';
import { MenuItemCardComponent } from '../menu-item-card/menu-item-card.component';

@Component({
  selector: 'app-menu-category',
  standalone: true,
  imports: [
    CommonModule,
    MenuItemCardComponent
  ],
  templateUrl: './menu-category.component.html',
  styleUrls: ['./menu-category.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class MenuCategoryComponent {
  @Input() title!: string;
  @Input() items: MenuItemDto[] = [];
  @Output() addToCart = new EventEmitter<{ item: MenuItemDto; quantity: number }>();
}
