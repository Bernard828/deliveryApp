import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { appRoutes } from './app.routes';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
//import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';

//PrimeNG
import Aura from '@primeuix/themes/aura';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(appRoutes),
    provideHttpClient(withInterceptorsFromDi()),
    //provideAnimationsAsync(),
    provideZoneChangeDetection({ eventCoalescing: true }),
  ]
};
