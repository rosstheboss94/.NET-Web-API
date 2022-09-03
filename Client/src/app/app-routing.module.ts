import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { JournalListComponent } from './components/journal-list/journal-list.component';
import { HomeComponent } from './components/home/home.component';
import { JournalFormComponent } from './components/forms/journal-form/journal-form.component';
import { TradeListComponent } from './components/trade-list/trade-list.component';
import { JournalEditComponent } from './components/journal-edit/journal-edit.component';

const routes: Routes = [
  {path: ':user/journals/:journal/edit', component: JournalEditComponent},
  {path: ':user/journals/:journal/trades', component: TradeListComponent},
  {path: ':user/journals/add', component: JournalFormComponent},
  {path: ':user/journals', component: JournalListComponent},
  {path: '', component: HomeComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
