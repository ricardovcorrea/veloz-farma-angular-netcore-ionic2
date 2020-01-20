import { Component } from '@angular/core';
import { NavParams, ViewController, LoadingController, ToastController } from 'ionic-angular';

import { Address } from '../../models/client';
import { SessionProvider } from '../../providers/session-provider';
import { BasePageComponent } from "../base-page.component";

import { Geolocation } from '@ionic-native/geolocation';

@Component({
  selector: 'page-address',
  templateUrl: 'address.html',
})
export class AddressPage extends BasePageComponent { 
  private address : Address = new Address();
  private isNew : boolean = true;
  private dontKnowMyCEP: boolean = false;
  
  constructor(loadingProvider: LoadingController, 
              toastProvider: ToastController,
              private geoLocator: Geolocation,  
              private sessionProvider: SessionProvider,
              private viewCtrl: ViewController, 
              private navParams: NavParams) { super(loadingProvider, toastProvider) }


  //Controles da pagina
  ionViewDidEnter() {
    let address = this.navParams.get('address');
    let useLocation = this.navParams.get('useLocation');

    if(address) {
      this.address = address;
      this.isNew = false;
    }
    else if(useLocation) {
      this.handleGeolocatorSucess({coords:{ latitude:-22.8749039, longitude:-43.2498879}});
      // this.geoLocator.getCurrentPosition({ maximumAge: 3000, timeout: 5000, enableHighAccuracy: true }).then(
      // (response) => this.handleGeolocatorSucess(response),
      // (error) =>  this.handleGeolocatorFail(error));
    }
  }
  
  private handleGeolocatorSucess(response){      
    this.address.Latitude = response.coords.latitude;
    this.address.Longitude = response.coords.longitude;
    this.completeAddress();
  }

  private handleGeolocatorFail(error){
    this.dismissLoading();    
    this.showToast("Ocorreu uma falha ao tentar adquirir sua posição, por favor tente novamente!");
  }

  closeModal() {
    this.viewCtrl.dismiss();
  }
  /////////////////////////////////////////////////////////////////

  

  //Botao de confirmar endereço
  confirmAddress() {
    this.showLoading().then(()=> {
      this.sessionProvider.addAddress(this.address).then(
      (response)=> this.handleAddAddressResponse(response),
      (error) => this.handleAsGenericError());
    });
  }

  handleAddAddressResponse(response) {
    this.dismissLoading();
    this.viewCtrl.dismiss();
  }
  /////////////////////////////////////////////////////////////////


  //Completa este endereco
  completeAddress() {
    this.showLoading().then(()=> {
      this.sessionProvider.completeAddress(this.address).then(
        (response) => this.handleCompleteAddressSucess(response),
        (error) => this.handleAsGenericError());
    });
  }

  handleCompleteAddressSucess(response){
      this.dismissLoading();
      this.address = response as Address;
  }
  /////////////////////////////////////////////////////////////////  
  

  //Dispara busca de endereco quando o cep chega a 8 digitos
  handleCepChanges(value:string) {
    if(value && value.length === 8)
      this.completeAddress();
  }
  /////////////////////////////////////////////////////////////////


}
