import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ToastrModule } from 'ngx-toastr';
import { PopoverModule } from 'ngx-bootstrap/popover';
import { AlertModule } from 'ngx-bootstrap/alert';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TradeListComponent } from './components/trade-list/trade-list.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavbarComponent } from './components/navbar/navbar.component';
import { HomeComponent } from './components/home/home.component';
import { FormsModule } from '@angular/forms';
import { RegisterModalComponent } from './components/modals/register-modal/register-modal.component';
import { JournalListComponent } from './components/journal-list/journal-list.component';
import { JournalFormComponent } from './components/forms/journal-form/journal-form.component';
import { TradeFormComponent } from './components/forms/trade-form/trade-form.component';
import { JournalEditComponent } from './components/journal-edit/journal-edit.component';
import { JwtInterceptor } from './interceptors/jwt.interceptor';
import { EditTradeFormComponent } from './components/forms/edit-trade-form/edit-trade-form.component';

@NgModule({
  declarations: [
    AppComponent,
    TradeListComponent,
    NavbarComponent,
    HomeComponent,
    RegisterModalComponent,
    JournalListComponent,
    JournalFormComponent,
    TradeFormComponent,
    JournalEditComponent,
    EditTradeFormComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    BsDropdownModule.forRoot(),
    ModalModule.forRoot(),
    ToastrModule.forRoot(),
    PopoverModule.forRoot(),
    AlertModule.forRoot()
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
