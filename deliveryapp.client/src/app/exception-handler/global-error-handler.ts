import { Injectable, Injector, ErrorHandler, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import{LoggingService}from'../../services/logging.service';

@Injectable({
  providedIn: 'root'
})
export class GlobalErrorHandler {

  private injector = inject(Injector);

  private loggingService=inject(LoggingService);

  handleError(error:any):void{
    const message= error.message?error.message:error.toString();
  // Log to console
console.error(error);

//Send error message

this.loggingService.log('ERROR', `Angular Error: ${message}`);
  }
}
