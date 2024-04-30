import { bootstrapApplication } from '@angular/platform-browser';
import { RootModule } from './app/root/root.module';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { environment } from './environments/environment';
import { enableProdMode } from '@angular/core';


export function getBaseUrl() {
  return document.getElementsByTagName('base')[0].href;
}
const providers = [
  { provide: 'BASE_URL', useFactory: getBaseUrl, deps: [] }
];

if (environment.production) {
  enableProdMode();
}

const bootstrap = () => {
    return platformBrowserDynamic().bootstrapModule(RootModule)
    .catch(err=>console.log(err));
};


// if (environment.production) {
//   console.log('Production Mode....');
  
// } else {
//   bootstrap(); // Regular bootstrap
// }
bootstrap();