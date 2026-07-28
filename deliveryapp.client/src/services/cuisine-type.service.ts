import { HttpClient } from '@angular/common/http';
import { Injectable, Service } from '@angular/core';
import { inject } from '@angular/core/primitives/di';
import { environment } from '../environments/environment.development';
import { CuisineType } from '../app/models/restuarant.model';
@Injectable({providedIn:'root'})
export class CuisineTypeService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.apiUrl}/CusineType`;

  getAll() {
    return this.http.get<CuisineType[]>(this.baseUrl + 'GetAll');
  }

  create(dto: CuisineType) {
    return this.http.post<CuisineType>(`${this.baseUrl}/Create`, dto);
  }

  update(id: number, dto: CuisineType) {
    return this.http.put<CuisineType>(`${this.baseUrl}/${id}`, dto);
  }

  delete(id: number) {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
  
}
