import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';
import { MenuItemCreateDto, MenuItemDto, MenuItemUpdateDto } from '../app/models/menu-item.model';
import { PagedResult } from '../app/models/paged-result.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OperatingHourService {

  private http = inject(HttpClient);
  private baseUrl = `${environment.apiUrl}/OperatingHour`;

  // getByRestaurantId(restaurantId: number): Observable<MenuItemDto[]> {
  //   return this.http.get<MenuItemDto[]>(
  //     `${this.baseUrl}/GetByRestaurant/${restaurantId}`
  //   );
  // }

  // getById(id: number): Observable<MenuItemDto> {
  //   return this.http.get<MenuItemDto>(`${this.baseUrl}/GetById/${id}`);
  // }

  // create(dto: MenuItemCreateDto):Observable<MenuItemDto> {
  //   return this.http.post<MenuItemDto>(`${this.baseUrl}/Create`, dto);
  // }

  // update(id: number, dto: MenuItemUpdateDto ): Observable<MenuItemDto> {
  //   return this.http.put<MenuItemDto>(`${this.baseUrl}/Update/${id}`, dto);
  // }

  // delete(id: number): Observable<void> {
  //   return this.http.delete<void>(`${this.baseUrl}/Delete/${id}`);
  // }
}
