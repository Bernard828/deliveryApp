import { ApplicationConfig, ErrorHandler, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { appRoutes } from './app.routes';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
//import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';

//PrimeNG
import Aura from '@primeuix/themes/aura';
import { GlobalErrorHandler } from './exception-handler/global-error-handler';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(appRoutes),
    provideHttpClient(withInterceptorsFromDi()),
    provideZoneChangeDetection({ eventCoalescing: true }),
    {provide:ErrorHandler, useClass:GlobalErrorHandler }
  ]
};
