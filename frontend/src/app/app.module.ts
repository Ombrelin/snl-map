import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {LeafletModule} from '@asymmetrik/ngx-leaflet';

import {AppComponent} from './app.component';
import {ServicesModule} from "./services/services.module";
import {CoreModule} from "./core/core.module";
import {HttpClientModule} from "@angular/common/http";
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatCardModule} from "@angular/material/card";
import {MatListModule} from "@angular/material/list";
import {SsvCommandModule} from "@ssv/ngx.command";
import {CityDetailComponent} from './components/city-detail/city-detail.component';
import {MatProgressSpinnerModule} from "@angular/material/progress-spinner";
import {MatButtonModule} from "@angular/material/button";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatAutocompleteModule} from "@angular/material/autocomplete";
import {MatOptionModule} from "@angular/material/core";
import {MatInputModule} from "@angular/material/input";
import {CitySearchComponent} from './components/city-search/city-search.component';
import {FormsModule} from "@angular/forms";
import {MatToolbarModule} from "@angular/material/toolbar";
import {AboutModalComponent} from './components/about-modal/about-modal.component';
import {MatDialogModule} from "@angular/material/dialog";

@NgModule({
  declarations: [
    AppComponent,
    CityDetailComponent,
    CitySearchComponent,
    AboutModalComponent
  ],
  imports: [
    BrowserModule,
    ServicesModule,
    CoreModule,
    HttpClientModule,
    LeafletModule,
    BrowserAnimationsModule,
    MatCardModule,
    MatListModule,
    SsvCommandModule,
    MatProgressSpinnerModule,
    MatButtonModule,
    MatFormFieldModule,
    MatAutocompleteModule,
    MatOptionModule,
    MatInputModule,
    FormsModule,
    MatToolbarModule,
    MatDialogModule
  ],
  providers: [AboutModalComponent],
  bootstrap: [AppComponent]
})
export class AppModule {
}
