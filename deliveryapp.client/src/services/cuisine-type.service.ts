import { HttpClient } from '@angular/common/http';
import { Injectable, Service } from '@angular/core';
import { inject } from '@angular/core/primitives/di';
import { environment } from '../environments/environment.development';
import { CuisineTypeDto } from '../app/models/cuisine-type.model';
import { Observable } from 'rxjs';

@Injectable({providedIn:'root'})
export class CuisineTypeService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.apiUrl}/CusineType`;

  getAll(): Observable<CuisineTypeDto[]> {
    return this.http.get<CuisineTypeDto[]>
      (`${this.baseUrl}/GetAll`);
  }

  create(dto: CuisineTypeDto) {
    return this.http.post<CuisineTypeDto>(`${this.baseUrl}/Create`, dto);
  }

  update(id: number, dto: CuisineTypeDto) {
    return this.http.put<CuisineTypeDto>(`${this.baseUrl}/Update/${id}`, dto);
  }

  delete(id: number) {
    return this.http.delete<void>(`${this.baseUrl}/Delete/${id}`);
  }
  
}
