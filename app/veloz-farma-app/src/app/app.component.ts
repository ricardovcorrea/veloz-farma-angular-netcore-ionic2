import { Component } from '@angular/core';
import { Platform} from 'ionic-angular';

import { StatusBar } from '@ionic-native/status-bar';
import { SplashScreen } from '@ionic-native/splash-screen';

import { HomePage } from './pages/home/home';
import { OrderPage } from './pages/order/order';

import { AddressPage } from './pages/address/address';

@Component({
  templateUrl: 'app.html'
})
export class AppFarma {
  rootPage = HomePage;

  constructor(platform: Platform) {
    platform.ready().then(() => {
    
    });
  }
}
