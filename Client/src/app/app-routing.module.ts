import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { JournalListComponent } from './components/journal-list/journal-list.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { HomeComponent } from './components/home/home.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { JournalFormComponent } from './components/forms/journal-form/journal-form.component';

const routes: Routes = [
  {path: ':user/journals/add', component: JournalFormComponent},
  {path: ':user/journals', component: JournalListComponent},
  {path: '', component: HomeComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
