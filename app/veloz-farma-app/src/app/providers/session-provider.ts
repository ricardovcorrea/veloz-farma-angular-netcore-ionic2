import { Injectable } from '@angular/core';
import { Client } from '../models/client';
import { Subject } from 'rxjs/Subject';
import { Observable } from 'rxjs/Observable';

import { Session } from '../models/session';
import { ConfigurationProvider } from './configuration-provider';
import { HttpProvider, HttpMethods } from './http-provider';
import { SearchProduct } from '../models/search';
import { Address } from '../models/client';
import { Sku } from '../models/product';

@Injectable()
export class SessionProvider{  

  private session : Session;
  public sessionObservable = new Subject<Session>();

  constructor(private httpProvider: HttpProvider, private configurationProvider: ConfigurationProvider) {
    this.session = new Session();
  }

  public getSession() : Observable<Session>
  {
    return this.httpProvider.execute(this.configurationProvider.getEndpointUrl("client","get"), HttpMethods.Get);
  }

  public setSession(session) : void
  {
    this.session = session;
    this.emitSessionChanges();
  }

  public emitSessionChanges()
  {
    this.sessionObservable.next(this.session);
  }

  public createClient(client: Client) : Promise<any>
  {
    return new Promise((resolve, reject) => {
      this.httpProvider.execute(this.configurationProvider.getEndpointUrl("client","create"), HttpMethods.Post, client).subscribe(
          (response)=> {
            this.setSession(response);
            resolve();
          },
          (error) => reject(error));
    });
  }

  public addAddress(address: Address) : Promise<any>
  {
    if(!this.session.Id)
    {
      let newClient = new Client();
      newClient.Addresses.push(address);
      return this.createClient(newClient);
    }
    else
    {
      return new Promise((resolve, reject) => {
      this.httpProvider.execute(this.configurationProvider.getEndpointUrl("order","addAddress"), HttpMethods.Post, address).subscribe(
        (response)=> {
          this.session.Addresses.push(response.AddressToShip);
          this.session.AtualOrder = response;
          this.emitSessionChanges();
          resolve(response)
        },
        (error) => reject(error))
    });
    }
  }

  public addProductToOrder(searchProduct: SearchProduct)
  {
    var tempSku = new Sku(searchProduct);
    this.httpProvider.execute(this.configurationProvider.getEndpointUrl("order","addProduct"), HttpMethods.Post, tempSku).subscribe(
      (response)=> {
        this.session.AtualOrder = response;
        this.emitSessionChanges();
      });
  }

  public completeAddress(addressToComplete: Address) : Promise<Address>
  {
    return new Promise((resolve, reject) => {
      this.httpProvider.execute(this.configurationProvider.getEndpointUrl("general","completeAddress"), HttpMethods.Post, addressToComplete).subscribe(
        (response)=> resolve(response),
        (error) => reject(error))
    }); 
  }

}
