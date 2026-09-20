import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { inject } from '@angular/core/';
import { environment } from '../environments/environment.development';
import { CuisineTypeCreateDto, CuisineTypeDto, CuisineTypeUpdateDto } from '../app/models/cuisine-type.model';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class CuisineTypeService {

  private readonly http = inject(HttpClient);

  private readonly baseUrl = `${environment.apiUrl}/CuisineType`;

  getAll(): Observable<CuisineTypeDto[]> {
    return this.http.get<CuisineTypeDto[]>
      (`${this.baseUrl}/GetAll`);
  }

  getById(id: number): Observable<CuisineTypeDto> {
    return this.http.get<CuisineTypeDto>(`${this.baseUrl}/GetById/${id}`);
  }

  create(dto: CuisineTypeCreateDto): Observable<CuisineTypeDto> {
    return this.http.post<CuisineTypeDto>(`${this.baseUrl}/Create`, dto);
  }

  update(id: number, dto: CuisineTypeUpdateDto): Observable<CuisineTypeDto> {
    return this.http.put<CuisineTypeDto>(`${this.baseUrl}/Update/${id}`, dto);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/Delete/${id}`);
  }

}
