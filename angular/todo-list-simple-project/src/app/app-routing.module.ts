import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TestApiComponent } from './components/test-api/test-api.component';

const routes: Routes = [
  {path:"test" ,component:TestApiComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
