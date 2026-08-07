import { Injectable, inject, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { finalize, tap } from 'rxjs';
import {
  RestaurantDto,
  RestaurantCreateDto,
} from '../app/models/restuarant.model';
import { environment } from '../environments/environment';
import { PagedResult } from '../app/models/paged-result.model';
//import { environment } from '../environments/environment.development';
//import { environment } from '../environments/environment.iis';

@Injectable({
  providedIn: 'root'
})
export class RestaurantService {
  private http = inject(HttpClient);
  private baseUrl = environment.apiUrl+'/Restaurant';

  restaurants = signal<RestaurantDto[]>([]);
  loading = signal(false);


  constructor() { }

  // OLD
  getAll() {
    this.loading.set(true);

    return this.http.get<RestaurantDto[]>(`${this.baseUrl}/GetAll`).pipe(
      tap(data => this.restaurants.set(data)),
      finalize(() => this.loading.set(false))
    );
  }

  getAllNew(params?: { page?: number; pageSize?: number; isActive?: boolean }) {
    return this.http.get<PagedResult<RestaurantDto>>(this.baseUrl, { params });
  }

  getById(id: number) {
    return this.http.get<RestaurantDto>(`${this.baseUrl}/${id}`);
  }

  create(dto: RestaurantCreateDto) {
    return this.http.post<RestaurantDto>(`${this.baseUrl}/Create`, dto)
      .pipe(
        tap(newRestaurant =>
          this.restaurants.update(list => [...list, newRestaurant])
        )
      );
  }

  createNew(dto: RestaurantDto) {
    return this.http.post<RestaurantDto>(`${this.baseUrl}/Create`, dto)
  }

  update(id: number, dto: RestaurantDto) {
    return this.http.put<RestaurantDto>(this.baseUrl + '/Update', dto)
      .pipe(
        tap(updated =>
          this.restaurants.update(list =>
            list.map(r => (r.restaurantId === id ? updated : r))
          )
        )
      );
  }

  updateNew(id: number, dto: RestaurantDto) {
    return this.http.put<RestaurantDto>(`${this.baseUrl}/${id}`, dto);
  }

  delete(id: number) {
    return this.http.delete(`${this.baseUrl}/${id}`).pipe(
      tap(() =>
        this.restaurants.update(list =>
          list.filter(r => r.restaurantId !== id)
        )
      )
    );
  }

  getMenuByRestaurantId(id: number) {
    return this.http.get(`${this.baseUrl}/${id}/menu`);
  }
}
