import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../app/models/user.model';
import { Environment } from '../environment/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  environemt = inject(Environment);
 // private userUri = `${environment}/User`;
  private apiUri = ``;
  private http = inject(HttpClient);
  constructor() { }

  registerUser(user: User): Observable<User> {
    return this.http.post<User>(`${this.apiUri}`, user);
  }
}
