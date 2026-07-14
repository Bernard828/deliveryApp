import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../app/environment/environment';
import { MenuItemSearchDto } from '../models/menuItem.model';

@Injectable({
  providedIn: 'root'
})
export class MenuItemService {
  private http = inject(HttpClient);
  private apiUrl = environment.apiUri + 'menuItems';
  constructor() { }

  getByRestaurantId(restaurantId: number) {
    return this.http.get<MenuItemSearchDto[]>(`${this.apiUrl}/restaurant/${restaurantId}`);
  }

  getById(id: number) {
    return this.http.get<MenuItemSearchDto>(`${this.apiUrl}/${id}`);
  }
}
