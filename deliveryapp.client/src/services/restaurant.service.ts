import { Injectable, inject, signal } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { finalize, tap, Observable } from 'rxjs';

import {
  RestaurantDto,
  RestaurantUpdateDto
} from '../app/models/restuarant.model';
import { RestaurantCreateDto } from '../app/models/restaurant-create.dto';
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


  getPaged(
    page: number = 1,
    pageSize: number = 10,
    isActive: boolean | null = true
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
  }

  update(id: number, dto: RestaurantUpdateDto): Observable<RestaurantDto> {
    return this.http.put<RestaurantDto>
      (`${this.baseUrl}/Update/${id}`, dto);
  }

  toggleActiveStatus(id: number): Observable<RestaurantDto> {
    return this.http.patch<RestaurantDto>
      (`${this.baseUrl}/ToggleActiveStatus/${id}`, {});
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/Delete/${id}`);
  }
}
