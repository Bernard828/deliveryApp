import { Injectable, inject, signal } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { finalize, tap, Observable } from 'rxjs';

import {
  RestaurantDto,
  RestaurantCreateDto,
} from '../app/models/restuarant.model';
import { PagedResult } from '../app/models/paged-result.model';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RestaurantService {
  private http = inject(HttpClient);
  private readonly baseUrl = environment.apiUrl + '/Restaurants';

  restaurants = signal<RestaurantDto[]>([]);

  readonly loading = signal(false);


  constructor() { }

  // OLD
  getAll() {
    this.loading.set(true);

    return this.http.get<RestaurantDto[]>(`${this.baseUrl}/GetAll`).pipe(
      tap(data => this.restaurants.set(data)),
      finalize(() => this.loading.set(false))
    );
  }

  getPaged(
    page: number,
    pageSize: number,
    isActive: boolean | null = null
  ): Observable<PagedResult<RestaurantDto>> {

    let params = new HttpParams()
      .set('page', page)
      .set('pageSize', pageSize);

    if (isActive !== null) {
      params = params.set('isActive', isActive);
    }

    this.loading.set(true);

    return this.http
      .get<PagedResult<RestaurantDto>>(`${this.baseUrl}/GetPaged/paged`,
        { params })
      .pipe(finalize(() => this.loading.set(false))
    );
  }

  getById(id: number) {
    return this.http.get<RestaurantDto>(`${this.baseUrl}/GetById/${id}`);
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
}
