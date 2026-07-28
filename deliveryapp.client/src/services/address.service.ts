import { HttpClient } from '@angular/common/http';
import { Injectable, Service } from '@angular/core';
import { inject } from '@angular/core/primitives/di';
import { environment } from '../environments/environment.development';
import { Address } from '../app/models/address.model';

@Injectable({ providedIn: 'root' })
export class AddressService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.apiUrl}/Address`

  getByRestaurant(restaurantId: number) {
    return this.http.get<Address[]>(`${this.baseUrl}/restaurant/${restaurantId}`);
  }

  create(dto: Address) {
    return this.http.post<Address>(this.baseUrl, dto);
  }

  update(id: number, dto: Address) {
    return this.http.put<Address>(`${this.baseUrl}/${id}`, dto);
  }

  delete(id: number) {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }

  
}
