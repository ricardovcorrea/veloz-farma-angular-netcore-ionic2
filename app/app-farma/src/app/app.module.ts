import { OneSignal } from '@ionic-native/onesignal';
import { CartPageModule } from './../pages/cart/cart.module';

import { FindBudgetPageModule } from './../pages/find-budget/find-budget.module';
import { SearchPageModule } from './../pages/search/search.module';
import { SingUpPageModule } from './../pages/sing-up/sing-up.module';
import { TutorialPageModule } from './../pages/tutorial/tutorial.module';
import { LoginPageModule } from './../pages/login/login.module';
import { EditAddressPageModule } from './../pages/edit-address/edit-address.module';
import { LocationPageModule } from './../pages/location/location.module';
import { ProfilePageModule } from './../pages/profile/profile.module';



import { BrowserModule } from '@angular/platform-browser';
import { ErrorHandler, NgModule, transition } from '@angular/core';
import { IonicApp, IonicErrorHandler, IonicModule } from 'ionic-angular';

import { MyApp } from './app.component';

import { StatusBar } from '@ionic-native/status-bar';
import { SplashScreen } from '@ionic-native/splash-screen';
import { CommonModule } from "@angular/common";
import { HttpModule } from "@angular/http";
import { IonicStorageModule } from '@ionic/storage';
import { ConfigurationProvider } from '../providers/configuration/configuration';
import { SessionProvider } from '../providers/session/session';
import { SearchProvider } from '../providers/search/search';
import { HomePageModule } from "../pages/home/home.module";

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { Geolocation } from '@ionic-native/geolocation';

import { Facebook } from '@ionic-native/facebook';

import { LOCALE_ID } from '@angular/core';

@NgModule({
  declarations: [
    MyApp
  ],
  imports: [
    BrowserModule,
    HttpModule,
    CommonModule,
    IonicStorageModule.forRoot(),
    BrowserAnimationsModule,
    IonicModule.forRoot(MyApp,{
      pageTransition: 'md-transition'
    }),
    HomePageModule,
    LocationPageModule,
    EditAddressPageModule,
    LoginPageModule,
    TutorialPageModule,
    SingUpPageModule,
    TutorialPageModule,
    SearchPageModule,
    CartPageModule,
    FindBudgetPageModule,
    ProfilePageModule
  ],
  bootstrap: [IonicApp],
  entryComponents: [
    MyApp
    ],
  providers: [
    StatusBar,
    SplashScreen,
    {provide: ErrorHandler, useClass: IonicErrorHandler},
    {provide: LOCALE_ID, useValue: "pt-Br"},
    ConfigurationProvider,
    SessionProvider,
    SearchProvider,
    Geolocation,
    Facebook,
    OneSignal
  ]
})
export class AppModule {}
