import { SessionProvider } from './../../providers/session/session';
import { ConfigurationProvider } from './../../providers/configuration/configuration';
import { Component } from '@angular/core';

@Component({
  selector: 'cart-button',
  templateUrl: 'cart-button.html'
})
export class CartButtonComponent {

  public cartItemsCount: number = 0;

  constructor(public configurationProvider: ConfigurationProvider, private SessionProvider: SessionProvider) {
    this.SessionProvider.Session.subscribe((session)=>{
      if(session && session.AtualOrder && session.AtualOrder.SkusRequesteds) {
        this.cartItemsCount = session.AtualOrder.SkusRequesteds.length;
      }
    });

    this.SessionProvider.EmmitSessionChanges();
  }

  openCart() {
    this.configurationProvider.app.getActiveNavs()[0].push("page-cart");
  }

}
