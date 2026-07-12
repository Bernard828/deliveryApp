import { inject, Injectable, signal } from '@angular/core';
import { RestaurantDto, RestaurantCreateDto, RestaurantSearchDto } from '../models/restuarant.model';
import { HttpClient } from '@angular/common/http';
import { Environment } from '../environment/environment';
import { Observable, tap } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class RestaurantService {
  private http = inject(HttpClient);
  private environement = inject(Environment);
  private apiUrl = 'https://loccalhost:7001/api/restaurants';
  constructor() { }

  //restaurants = this.http.get<RestaurantDto[]>(this.environement.apiUri + 'api/restaurant');
  restaurants = signal<RestaurantDto[]>([]);
  loading = signal<boolean>(false);

  getAll(): Observable<RestaurantDto[]> {
    this.loading.set(true);
    return this.http.get<RestaurantDto[]>(this.apiUrl).pipe(
      tap({
        next: (data) => this.restaurants.set(data),
        finalize: () => this.loading.set(false)
      })
    );
  }

  getbyId(id: number): Observable<RestaurantSearchDto> {
    return this.http.get<RestaurantSearchDto>(`${this.apiUrl}/${id}`);
  }

  create(dto: RestaurantCreateDto): Observable<RestaurantDto> {
    return this.http.post<RestaurantDto>(this.apiUrl, dto).pipe(
      tap((newDocument) => this.restaurants.update(list => [...list, newDocument]))
    );
  }
}
