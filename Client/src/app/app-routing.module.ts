import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { JournalListComponent } from './components/journal-list/journal-list.component';
import { HomeComponent } from './components/home/home.component';
import { JournalFormComponent } from './components/forms/journal-form/journal-form.component';
import { TradeListComponent } from './components/trade-list/trade-list.component';
import { JournalEditComponent } from './components/journal-edit/journal-edit.component';
import { AuthGuard } from './guards/auth.guard';
import { TradeFormComponent } from './components/forms/trade-form/trade-form.component';
import { EditTradeFormComponent } from './components/forms/edit-trade-form/edit-trade-form.component';

const routes: Routes = [
  {
    path: ':user/journals/:journal/trades/edit',
    component: EditTradeFormComponent,
    canActivate: [AuthGuard],
  },
  {
    path: ':user/journals/:journal/trades/add',
    component: TradeFormComponent,
    canActivate: [AuthGuard],
  },
  {
    path: ':user/journals/:journal/edit',
    component: JournalEditComponent,
    canActivate: [AuthGuard],
  },
  {
    path: ':user/journals/:journal/trades',
    component: TradeListComponent,
    canActivate: [AuthGuard],
  },
  {
    path: ':user/journals/add',
    component: JournalFormComponent,
    canActivate: [AuthGuard],
  },
  {
    path: ':user/journals',
    component: JournalListComponent,
    canActivate: [AuthGuard],
  },
  { path: '', component: HomeComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
