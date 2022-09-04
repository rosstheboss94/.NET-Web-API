import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap/modal';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TradeListComponent } from './components/trade-list/trade-list.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavbarComponent } from './components/navbar/navbar.component';
import { HomeComponent } from './components/home/home.component';
import { FormsModule } from '@angular/forms';
import { RegisterModalComponent } from './components/modals/register-modal/register-modal.component';
import { JournalListComponent } from './components/journal-list/journal-list.component';
import { JournalCardComponent } from './components/cards/journal-card/journal-card.component';
import { TradeCardComponent } from './components/cards/trade-card/trade-card.component';
import { JournalFormComponent } from './components/forms/journal-form/journal-form.component';
import { TradeFormComponent } from './components/forms/trade-form/trade-form.component';
import { JournalEditComponent } from './components/journal-edit/journal-edit.component';
import { JwtInterceptor } from './interceptors/jwt.interceptor';
import { DeleteAlertComponent } from './components/alerts/delete-alert/delete-alert.component';

@NgModule({
  declarations: [
    AppComponent,
    TradeListComponent,
    NavbarComponent,
    HomeComponent,
    RegisterModalComponent,
    JournalListComponent,
    JournalCardComponent,
    TradeCardComponent,
    JournalFormComponent,
    TradeFormComponent,
    JournalEditComponent,
    DeleteAlertComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    BsDropdownModule.forRoot(),
    ModalModule.forRoot()
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
