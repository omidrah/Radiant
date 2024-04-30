import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TablistComponent } from './tablist/tablist.component';

const routes: Routes = [
  {
      path: '',component: TablistComponent,
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeRoutingModule { }
