import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { inject } from '@angular/core/';
import { environment } from '../environments/environment.development';
import { RestaurantTagDto } from '../app/models/restaurant-tag-dto.model';
import { RestaurantTagAssignmentDto } from '../app/models/restaurant-tag-assignment-dto.model';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class RestaurantTagService {

  private http = inject(HttpClient);

  private baseUrl = `${environment.apiUrl}/RestaurantTag`

  getAll(): Observable<RestaurantTagDto[]> { return this.http.get<RestaurantTagDto[]>(`${this.baseUrl}/GetAll`); }

  getByRestaurant(restaurantId: number): Observable<RestaurantTagDto[]> {
    return this.http.post<RestaurantTagDto[]>(`${this.baseUrl}/GetByRestaurant/${restaurantId}`, {});
  }

  create(name:string):Observable<RestaurantTagDto> {
    return this.http.post<RestaurantTagDto>(this.baseUrl, {name});
  }

  update(id: number, name: string):Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/Update/${id}`, {name});
  }

  delete(id: number):Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/Delete/${id}`);
  }



  assignToRestaurant(restaurantId:number, restaurantTagIds:number[]):Observable<void>{{

    const dto:RestaurantTagAssignmentDto={

      restaurantId,

      restaurantTagIds
    };

    return this.http.put<void>(`${this.baseUrl}/AssignToRestaurant`, dto);
  }}
}
