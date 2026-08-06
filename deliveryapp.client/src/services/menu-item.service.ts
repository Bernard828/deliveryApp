import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';
import { MenuItemDto } from '../app/models/menuItem.model';
import { PagedResult } from '../app/models/paged-result.model';

@Injectable({
  providedIn: 'root'
})
export class MenuItemService {
  private http = inject(HttpClient);
  private baseUrl = environment.apiUrl + 'MenuItem';
  constructor() { }

  create(dto: MenuItemDto) {
    return this.http.post<MenuItemDto>(this.baseUrl + 'Create', dto);
  }

  update(id: number, dto: MenuItemDto) {
    return this.http.put<MenuItemDto>(`${this.baseUrl}/${id}`, dto);
  }

  delete(id: number) {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }

  getByRestaurantId(restaurantId: number, params?: { page?: number; pageSize?: number;isActive?:boolean }) {
    //return this.http.get<MenuItemDto[]>(`${this.apiUrl}/restaurant/${restaurantId}`);
    return this.http.get<PagedResult<MenuItemDto>>(
      `${this.baseUrl}/restaurant/${restaurantId}`,
      { params }
    );
  }

  getById(id: number) {
    return this.http.get<MenuItemDto>(`${this.baseUrl}/${id}`);
  }
}
