import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ArabicComponent } from './arabic/arabic.component';
import { EnglishComponent } from './english/english.component';

const routes: Routes = [
  {path:`ara`,component:ArabicComponent},
  {path:`en`,component:EnglishComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
