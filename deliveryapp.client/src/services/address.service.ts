import { HttpClient } from '@angular/common/http';
import { Injectable, Service } from '@angular/core';
import { inject } from '@angular/core/primitives/di';
import { environment } from '../environments/environment.development';
import { Address, AddressDto } from '../app/models/address.model';

@Injectable({ providedIn: 'root' })
export class AddressService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.apiUrl}/Address`

  getByRestaurant(restaurantId: number) {
    return this.http.get<AddressDto[]>(`${this.baseUrl}/restaurant/${restaurantId}`);
  }

  create(dto: Address) {
    return this.http.post<AddressDto>(this.baseUrl, dto);
  }

  update(id: number, dto: AddressDto) {
    return this.http.put<AddressDto>(`${this.baseUrl}/${id}`, dto);
  }

  delete(id: number) {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
