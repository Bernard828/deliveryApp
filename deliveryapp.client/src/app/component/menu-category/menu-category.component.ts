import {
  Component,
  Input,
  ChangeDetectionStrategy
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { MenuItemSearchDto } from '../../models/menuItem.model';
import { MenuItemCardComponent } from '../menu-item-card/menu-item-card.component';

@Component({
  selector: 'app-menu-category',
  standalone: true,
  imports:[CommonModule,MenuItemCardComponent],
  templateUrl: './menu-category.component.html',
  styleUrls: ['./menu-category.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class MenuCategoryComponent {
  @Input() title!: string;
  @Input() items: MenuItemSearchDto[] = [];
}
