import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';
import { PagedResult } from '../app/models/paged-result.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoggingService {

  private http = inject(HttpClient);
  private baseUrl = `${environment.apiUrl}/Log`;

  log(level: 'INFO'|'DEBUG'|'WARN'|'ERROR', message:string){

    this.http.post(`${this.baseUrl}/PostLog`, {level, message}).subscribe({

      error:(err)=>console.error('Failed to send log to server:', err)
    });
  }
}
