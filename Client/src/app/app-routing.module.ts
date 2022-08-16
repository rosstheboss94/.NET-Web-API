import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { JournalsListComponent } from './components/journals-list/journals-list.component';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  { path: ':username/journals', component: JournalsListComponent },
  { path: '', component: HomeComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
