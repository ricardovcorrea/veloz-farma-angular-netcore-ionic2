import { Sku } from './../../models/product';
import { SearchProduct } from './../../models/search';
import { Client, Address } from './../../models/client';
import { Session } from './../../models/session';
import { ConfigurationProvider } from './../configuration/configuration';
import { Injectable } from '@angular/core';
import { Subject } from "rxjs/Subject";

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/timeout';
import 'rxjs/add/operator/take';

@Injectable()
export class SessionProvider {

  public Session = new Subject<Session>();
  public _Session = new Session();

  constructor(public configurationProvider: ConfigurationProvider) {
  }

  public LoadSession() : Promise<Session>{
    return new Promise<Session>((resolve)=>{
      this.configurationProvider.storage.get("Session").then((SessionString)=>{
        if(SessionString) {
          this._Session = JSON.parse(SessionString);
          this.SaveSessionChanges();
          resolve(this._Session);
        }
        resolve(null);
      });
    });
  }
  
  public CreateSession(client: Client) : Promise<any> {
    let endpoint = this.configurationProvider.getEndpointUrl("client","create");
    return this.configurationProvider.getRequestConfiguration().then((options) => {

      return this.configurationProvider.http.post(endpoint, client, options).timeout(30000).map((response) => {
        
        let sessionKeyHeader = response.headers.get("Session");
        if(sessionKeyHeader) {

          this.configurationProvider.DebugLogAction("Set Session Token");
          this.configurationProvider.DebugLogAction(sessionKeyHeader);

          this.configurationProvider.storage.set("SessionToken",sessionKeyHeader); 
        }

        this._Session = response.json();
        this.SaveSessionChanges();
        return this._Session;
        
      }).take(1).toPromise();

    });
  }

  public Login(credentials: Client) : Promise<any> {
    let endpoint = this.configurationProvider.getEndpointUrl("client","login") + "?email="+credentials.Email+"&password="+credentials.Password;
    return this.configurationProvider.getRequestConfiguration().then((options) => {
      return this.configurationProvider.http.get(endpoint, options).timeout(30000).map((response) => {

        let sessionKeyHeader = response.headers.get("Session");
        if(sessionKeyHeader) {

          this.configurationProvider.DebugLogAction("Set Session Token");
          this.configurationProvider.DebugLogAction(sessionKeyHeader);
          
          this.configurationProvider.storage.set("SessionToken", sessionKeyHeader); 
        }

        this._Session = response.json();
        this.SaveSessionChanges();
        return this._Session;
      }).take(1).toPromise();
    });
  }

  public AddAddress(Address: Address) : Promise<any> {
    let endpoint = this.configurationProvider.getEndpointUrl("client","addAddress");
    return this.configurationProvider.getRequestConfiguration().then((options) => {
      return this.configurationProvider.http.post(endpoint, Address, options).timeout(30000).map((response) => {
        this._Session.AtualOrder.AddressToShip = response.json();
        this.SaveSessionChanges();
        return this._Session;
      }).take(1).toPromise();
    });
  }

  public SetAddress(Address: Address) : Promise<any> {
    let endpoint = this.configurationProvider.getEndpointUrl("order","addAddress") + "/" + Address.Id;
    return this.configurationProvider.getRequestConfiguration().then((options) => {
      return this.configurationProvider.http.post(endpoint, null, options).timeout(30000).map((response) => {
        this._Session.AtualOrder = response.json();
        this.SaveSessionChanges();
        return this._Session;
      }).take(1).toPromise();
    });
  }
  
  public addProductToCart(searchProduct: SearchProduct)
  {
    return this.addSkuToCart(new Sku(searchProduct));
  }

  public addSkuToCart(sku: Sku)
  {
    let endpoint = this.configurationProvider.getEndpointUrl("order","addProduct");
    return this.configurationProvider.getRequestConfiguration().then((options) => {
      return this.configurationProvider.http.post(endpoint, sku, options).timeout(30000).map((response) => {
        this._Session.AtualOrder = response.json();
        this.SaveSessionChanges();
        this.configurationProvider.ShowToast("Produto adicionado ao carrinho!", "bottom");
        return this._Session;
      }).take(1).toPromise();
    });
  }

  public SaveSessionChanges() {
    this.configurationProvider.storage.set("Session", JSON.stringify(this._Session));
    this.EmmitSessionChanges();
  }

  public EmmitSessionChanges() {
    this.Session.next(this._Session);
  }

}
