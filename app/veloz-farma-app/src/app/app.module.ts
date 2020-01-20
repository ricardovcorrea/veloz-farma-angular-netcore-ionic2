import { NgModule, ErrorHandler } from '@angular/core';
import { IonicApp, IonicModule, IonicErrorHandler } from 'ionic-angular';

import { Geolocation } from '@ionic-native/geolocation';

import { StatusBar } from '@ionic-native/status-bar';
import { SplashScreen } from '@ionic-native/splash-screen';

import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';

import { AppFarma } from './app.component';

import { LocationModalProvider } from './providers/location-modal-provider';
import { SearchProvider } from './providers/search-provider';
import { ConfigurationProvider } from './providers/configuration-provider';
import { SessionProvider } from './providers/session-provider'

import { HttpProvider } from './providers/http-provider'

import { LimitTextSizePipe } from './pipes/limit-text-size.pipe';
import { LeftZeroesPipe } from './pipes/left-zeroes-pipe';

import { AddressBarComponent } from './components/address-bar/address-bar.component';
import { HomeSearchBarComponent } from './components/home-search-bar/home-search-bar.component';
import { CategoryListComponent } from './components/category-list/category-list.component';
import { ProductListComponent } from './components/product-list/product-list.component';
import { OrderFabComponent } from './components/order-fab/order-fab.component';
import { OrderProductListComponent } from './components/order-product-list/order-product-list.component';
import { BudgetTimerComponent } from './components/budget-timer/budget-timer.component';
import { BudgetListComponent } from './components/budget-list/budget-list.component';

import { HomePage } from './pages/home/home';
import { LocationModal } from './pages/location-modal/location-modal';
import { SearchPage } from './pages/search/search';
import { OrdersPage } from './pages/orders/orders';
import { BudgetsPage } from './pages/budgets/budgets';

import { AddressPageModule } from './pages/address/address.module';

import { CircleProgressComponent } from './components/circle-progress/circle-progress.component';

import { Storage } from '@ionic/storage';

@NgModule({
  declarations: [
    AppFarma,
    LimitTextSizePipe,
    LeftZeroesPipe,
    AddressBarComponent,
    HomeSearchBarComponent,
    CategoryListComponent,
    ProductListComponent,
    OrderFabComponent,
    OrderProductListComponent,
    BudgetTimerComponent,
    CircleProgressComponent,
    BudgetListComponent,
    HomePage,
    LocationModal,
    SearchPage,
    OrdersPage,
    BudgetsPage
  ],
  imports: [
    BrowserModule,
    HttpModule,
    IonicModule.forRoot(AppFarma,{
     mode: "md" 
    }),
    AddressPageModule
  ],
  bootstrap: [IonicApp],
  entryComponents: [
    AppFarma,
    HomePage,
    LocationModal,
    SearchPage,
    OrdersPage,
    BudgetsPage
  ],
  providers: [{provide: ErrorHandler, useClass: IonicErrorHandler},
              StatusBar,
              SplashScreen,
              Geolocation,
              Storage,
              HttpProvider,
              ConfigurationProvider, 
              LocationModalProvider, 
              SearchProvider, 
              SessionProvider]
})
export class AppFarmaModule {}
