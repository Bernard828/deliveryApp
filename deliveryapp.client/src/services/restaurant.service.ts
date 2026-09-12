import { Injectable, inject, signal } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { finalize, tap, Observable } from 'rxjs';

import {
  RestaurantDto,
  RestaurantCreateDto,
  RestaurantUpdateDto
} from '../app/models/restuarant.model';

import { PagedResult } from '../app/models/paged-result.model';

import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RestaurantService {
  private readonly baseUrl = `${environment.apiUrl}/Restaurants`;

  // readonly loading = false;

  private readonly http = inject(HttpClient);

  restaurants = signal<RestaurantDto[]>([]);

  readonly loading = signal(false);


  // OLD
  // getAll() {
  //   this.loading.set(true);

  //   return this.http.get<RestaurantDto[]>(`${this.baseUrl}/GetAll`).pipe(
  //     tap(data => this.restaurants.set(data)),
  //     finalize(() => this.loading.set(false))
  //   );
  // }

  getPaged(
    page: number = 1,
    pageSize: number = 10,
    isActive: boolean | null = null
  ): Observable<PagedResult<RestaurantDto>> {

    let params = new HttpParams()
      .set('page', page)
      .set('pageSize', pageSize);

    if (isActive !== null) {
      params = params.set
        ('isActive',
          isActive);
    }

    this.loading.set(true);

    return this.http
      .get<PagedResult<RestaurantDto>>(
        `${this.baseUrl}/GetPaged/paged`,
        { params }
      )
      .pipe(finalize(() =>
        this.loading.set(false))
      );
  }

  getById(id: number): Observable<RestaurantDto> {
    return this.http.get<RestaurantDto>
      (`${this.baseUrl}/GetById/${id}`);
  }

  create(dto: RestaurantCreateDto): Observable<RestaurantDto> {
    return this.http.post<RestaurantDto>(`${this.baseUrl}/Create`, dto);
    // .pipe(
    //   tap(newRestaurant =>
    //     this.restaurants.update(list => [...list, newRestaurant])
    //   )
    // );
  }

  update(id: number, dto: RestaurantUpdateDto): Observable<void> {
    return this.http.put<void>
      (`${this.baseUrl}/Update/${id}`, dto);
    // .pipe(
    //   tap(updated =>
    //     this.restaurants.update(list =>
    //       list.map(r => (r.restaurantId === id ? updated : r))
    //     )
    //   )
    // );
  }

  // updateNew(id: number, dto: RestaurantDto) {
  //   return this.http.put<RestaurantDto>(`${this.baseUrl}/${id}`, dto);
  // }

  toggleActiveStatus(id: number): Observable<void> {
    return this.http.patch<void>
      (`${this.baseUrl}/ToggleActiveStatus/${id}`, {});
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/Delete/${id}`);
    //   .pipe(
    //   tap(() =>
    //     this.restaurants.update(list =>
    //       list.filter(r => r.restaurantId !== id)
    //     )
    //   )
    // );
  }
}
