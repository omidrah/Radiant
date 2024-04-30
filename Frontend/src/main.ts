import { bootstrapApplication } from '@angular/platform-browser';
import { RootModule } from './app/root/root.module';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { environment } from './environments/environment';
import { enableProdMode } from '@angular/core';

if (environment.production) {
  enableProdMode();
}

const bootstrap = () => {
    return platformBrowserDynamic().bootstrapModule(RootModule);
};


// if (environment.production) {
//   console.log('Production Mode....');
  
// } else {
//   bootstrap(); // Regular bootstrap
// }
bootstrap();