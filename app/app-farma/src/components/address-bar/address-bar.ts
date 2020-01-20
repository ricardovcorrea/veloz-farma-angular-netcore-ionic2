import { ConfigurationProvider } from './../../providers/configuration/configuration';
import { SessionProvider } from './../../providers/session/session';
import { Address } from './../../models/client';
import { Component, Input } from '@angular/core';
import { Observable } from "rxjs/Observable";

@Component({
  selector: 'address-bar',
  templateUrl: 'address-bar.html'
})
export class AddressBarComponent {

  private AddressText: string = "";

  constructor(public configurationProvider: ConfigurationProvider, private SessionProvider: SessionProvider) {
    this.SessionProvider.Session.subscribe((session)=>{
      if(session && session.AtualOrder && session.AtualOrder.AddressToShip) {
        this.AddressText = session.AtualOrder.AddressToShip.Street + ", " + session.AtualOrder.AddressToShip.Number + " - " + session.AtualOrder.AddressToShip.City
      }
    });

    this.SessionProvider.EmmitSessionChanges();
  }
  
  changeAddress() {
    this.configurationProvider.app.getActiveNavs()[0].push("page-location",  { CanClose: false } );
  }

}
