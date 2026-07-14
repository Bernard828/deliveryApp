import { Injectable, inject, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { finalize, tap } from 'rxjs';
import {
  RestaurantDto,
  RestaurantCreateDto,
  RestaurantSearchDto
} from '../models/restuarant.model';
import { environment } from '../environment/environment';

@Injectable({
  providedIn: 'root'
})
export class RestaurantService {
  private http = inject(HttpClient);
  private apiUrl = environment.apiUri + ' restaurants';

  restaurants = signal<RestaurantDto[]>([]);
  loading = signal(false);

  constructor() { }

  getAll() {
    this.loading.set(true);

    return this.http.get<RestaurantDto[]>(this.apiUrl).pipe(
      tap(data => this.restaurants.set(data)),
      finalize(() => this.loading.set(false))
    );
  }

  getbyId(id: number) {
    return this.http.get<RestaurantSearchDto>(`${this.apiUrl}/${id}`);
  }

  create(dto: RestaurantCreateDto) {
    return this.http.post<RestaurantDto>(this.apiUrl, dto).pipe(
      tap(newRestaurant =>
        this.restaurants.update(list => [...list, newRestaurant])
      )
    );
  }

  update(id: number, dto: RestaurantDto) {
    return this.http.put<RestaurantDto>(`${this.apiUrl}/${id}`, dto).pipe(
      tap(updated =>
        this.restaurants.update(list =>
          list.map(r => (r.restaurantId === id ? updated : r))
        )
      )
    );
  }

  delete(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`).pipe(
      tap(() =>
        this.restaurants.update(list =>
          list.filter(r => r.restaurantId !== id)
        )
      )
    );
  }

  getMenuByRestaurantId(id: number) {
    return this.http.get(`${this.apiUrl}/${id}/menu`);
  }
}
