import { Sku } from './../../models/product';
import { Order } from './../../models/order';
import { SessionProvider } from './../../providers/session/session';
import { ConfigurationProvider } from './../../providers/configuration/configuration';
import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';

@IonicPage({ name:"page-cart"})
@Component({
  selector: 'page-cart',
  templateUrl: 'cart.html',
})
export class CartPage {
  public cart : Order = new Order();
  public IsLoading : boolean;

  constructor(public configurationProvider: ConfigurationProvider, private SessionProvider: SessionProvider) {
    this.IsLoading = true;
    this.SessionProvider.Session.subscribe((session)=>{
      if(session && session.AtualOrder && session.AtualOrder) {
        this.cart = session.AtualOrder;
      }
      this.IsLoading = false;
    });

    this.SessionProvider.EmmitSessionChanges();
  }
  
  addMoreProducts() {
    this.configurationProvider.app.getActiveNavs()[0].pop();
  }
  
findBudgets(){
  this.configurationProvider.app.getActiveNavs()[0].push("page-find-budget");
}
  
}
