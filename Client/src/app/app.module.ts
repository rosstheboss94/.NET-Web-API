import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap/modal';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TradeListComponent } from './trade-list/trade-list.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavbarComponent } from './components/navbar/navbar.component';
import { HomeComponent } from './components/home/home.component';
import { FormsModule } from '@angular/forms';
import { RegisterModalComponent } from './components/modals/register-modal/register-modal.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { JournalListComponent } from './components/journal-list/journal-list.component';
import { JournalCardComponent } from './components/cards/journal-card/journal-card.component';
import { TradeCardComponent } from './components/cards/trade-card/trade-card.component';
import { JournalFormComponent } from './components/forms/journal-form/journal-form.component';

@NgModule({
  declarations: [
    AppComponent,
    TradeListComponent,
    NavbarComponent,
    HomeComponent,
    RegisterModalComponent,
    SidebarComponent,
    JournalListComponent,
    JournalCardComponent,
    TradeCardComponent,
    JournalFormComponent
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
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
