import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { AuthGuard } from './_guards/AuthGuard';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { LoginComponent } from './login/login.component';
import { LandmarkremarkComponent } from './landmarkremark/landmarkremark.component';
import { AgmCoreModule } from '@agm/core';
import { MarkerSearchComponent } from './marker-search/marker-search.component';
import { FilterPipe } from './marker-search/filter.pipe';
import { MapContentComponent } from './landmarkremark/map.component';
import { JwtInterceptor } from './helpers/jwt.interceptor';
import { UserService } from './services/user-service.service';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    LoginComponent,
    LandmarkremarkComponent,
    MarkerSearchComponent,
    FilterPipe,
    MapContentComponent
    
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: LoginComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'landmarkremark', component: LandmarkremarkComponent, canActivate: [AuthGuard] },
      // otherwise redirect to home
      { path: '**', redirectTo: '' }
    ]),
     // Google cloud Api key to display Google Maps
     AgmCoreModule.forRoot({
      apiKey: 'AIzaSyBfSbbvG6EXEtgav8sqSoYNoPSSKuSpG0Q'
    }),
  ],
  providers: [
    AuthGuard,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true
    },
    UserService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
