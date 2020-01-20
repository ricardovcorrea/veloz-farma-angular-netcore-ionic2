import { Address } from './../../models/client';
import { Injectable } from '@angular/core';
import { Http, RequestOptions, Headers } from '@angular/http';

import { Facebook } from '@ionic-native/facebook';

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/timeout';
import 'rxjs/add/operator/take';

import { Storage } from '@ionic/storage';
import { ToastController, ModalController, MenuController, Platform, App } from "ionic-angular";

import { Geolocation } from '@ionic-native/geolocation';
import { OneSignal } from '@ionic-native/onesignal';

@Injectable()
export class ConfigurationProvider {

  private DebugModeOn: boolean = true;

  private apiBaseProtocol: string = "http";
  private apiBaseUrl: string = "ihchegou.mobfiq.com.br";
  private apiBasePort: number = 80;
  private apiEndpoints = {
    general: {
      completeAddress:"api/pub/address/info"
    },
    search: {
      fulltext: "api/pub/fulltext"
    },
    client: {
      login:"api/pub/client/login",
      create: "api/pub/client",
      get: "api/pub/client",
      addAddress: "api/pub/address"
    },
    order:{
      addAddress: "api/pub/order/address",
      addProduct: "api/pub/order/sku"
    }
  }

  constructor(public http: Http, 
              public storage: Storage, 
              public toastController: ToastController, 
              public modalController: ModalController, 
              public menuController: MenuController, 
              public geoLocator: Geolocation, 
              public platform: Platform, 
              public facebookProvider: Facebook,
              public app: App,
              public oneSignal: OneSignal) {}

  public getEndpointUrl(context: string, action: string) : string
  {
    return this.apiEndpoints[context][action] ? this.getBaseUrl() + this.apiEndpoints[context][action] : '';
  }

  private getBaseUrl() : string
  {
    return this.apiBaseProtocol + "://" +this.apiBaseUrl + ":" + this.apiBasePort +"/";
  }

  public getRequestConfiguration() : Promise<RequestOptions> {
    return new Promise<RequestOptions>((resolve) =>{
      let requestOptions = new RequestOptions();
      requestOptions.headers = new Headers();
      requestOptions.headers.append("Content-Type","application/json");
      this.storage.get("SessionToken").then((SessionToken) => {
        if(SessionToken) {
          this.DebugLogAction("Get Session Token");
          this.DebugLogAction(SessionToken);
          requestOptions.headers.append("Session", SessionToken);
        }
        resolve(requestOptions);
      });
    });
  }
  
  public completeAddress(addressToComplete: Address) : Promise<Address> {
    return this.getRequestConfiguration().then((options) => {
      let endpoint = this.getEndpointUrl("general","completeAddress");
      return this.http.post(endpoint, addressToComplete, options).timeout(30000).map(response => response.json()).take(1).toPromise();
    });
  }
  
  public ShowToast(_message: string, position?:string) {
    let newToast = this.toastController.create({
      message: _message,
      position: position || "top",
      duration: 3000
    });

    newToast.present();
  }

  public DebugLogAction(action){
    if(this.DebugModeOn){
      console.log(action);
    }
  }

}
