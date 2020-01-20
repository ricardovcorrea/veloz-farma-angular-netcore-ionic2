import { Component, Input } from '@angular/core';
import { SessionProvider } from '../../providers/session-provider';
import { LocationModalProvider } from '../../providers/location-modal-provider';
import { Address } from '../../models/client';

@Component({
  selector: 'address-bar',
  templateUrl: '/address-bar.component.html',
  styleUrls: ['/address-bar.component.scss']
})
export class AddressBarComponent {
  @Input() limitSize : boolean = true;

  private addressString : string = "";

  constructor(private sessionProvider: SessionProvider, private locationModalProvider: LocationModalProvider) {
    
    this.sessionProvider.sessionObservable.subscribe((result)=>{
      if (!result.AtualOrder)
          return;
          
      var address = Object.assign(new Address(), result.AtualOrder.AddressToShip);
      this.addressString = address.getAddressString();
    });

    this.sessionProvider.emitSessionChanges();
  }
  
  changeLocation()
  {
    this.locationModalProvider.openLocationModal();
  }
  
}
