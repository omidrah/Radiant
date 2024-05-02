import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
    { path: 'home', redirectTo: '', pathMatch: 'full' },    
    {
        path: '',
        loadChildren: () => import('../home/home.module').then(m => m.HomeModule), // Lazy load account module
        data: { preload: true }
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
    providers: []
})
export class RootRoutingModule { }

