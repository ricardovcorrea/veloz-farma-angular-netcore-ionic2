import { SessionProvider } from './../../providers/session/session';
import { ConfigurationProvider } from './../../providers/configuration/configuration';
import { Address, Client } from './../../models/client';
import { Component } from '@angular/core';
import { IonicPage, NavParams, ViewController, App } from 'ionic-angular';

@IonicPage({ name:"page-edit-address"})
@Component({
  selector: 'page-edit-address',
  templateUrl: 'edit-address.html',
})
export class EditAddressPage {
  private address : Address = new Address();
  private IsLoading: boolean = false;

  constructor(private app: App, private viewCtrl: ViewController, public configurationProvider: ConfigurationProvider, public sessionProvider: SessionProvider) {
  
  }

  closeModal() {
    this.viewCtrl.dismiss();
  }

  saveAndReturn() {
    if(!this.sessionProvider._Session.Id) {
      this.IsLoading = true;
      let newClient = new Client();
      newClient.Addresses.push(this.address);
      this.sessionProvider.CreateSession(newClient).then(() => {
        this.IsLoading = false;
        this.viewCtrl.dismiss().then(()=>{
          this.app.getActiveNav().setRoot("page-home");
        });
        
      },()=>{
        this.IsLoading = false;
      });
    } else {
      this.sessionProvider.AddAddress(this.address).then(() => {
        this.IsLoading = false;
        this.viewCtrl.dismiss().then(()=>{
          this.app.getActiveNav().setRoot("page-home");
        });
      },()=>{
        this.IsLoading = false;
        this.configurationProvider.ShowToast("Ocorreu uma falha ao adicionar o endereço!");
      });
    }
  }

  //Dispara busca de endereco quando o cep chega a 8 digitos
  handleCepChanges(value:string) {
    if(value && value.length === 8)
      this.completeAddress();
  }
  /////////////////////////////////////////////////////////////////
  
  useMyLocation() {
    this.IsLoading = true;
    if(this.configurationProvider.platform.is("cordova")) {
      this.configurationProvider.geoLocator.getCurrentPosition({ maximumAge: 3000, timeout: 30000, enableHighAccuracy: true }).then(
        (response) => this.handleGeolocatorSucess(response),
        (error) => {
          this.IsLoading = false;
          this.configurationProvider.ShowToast("Ocorreu uma falha ao tentar adquirir sua posição, por favor tente novamente!")
        });
    } else {
      this.handleGeolocatorSucess({coords:{ latitude:-22.8749039, longitude:-43.2498879}});
    }
  }

  private handleGeolocatorSucess(response){      
    this.address.Latitude = response.coords.latitude;
    this.address.Longitude = response.coords.longitude;
    this.address.PostalCode = null;
    this.completeAddress();
  }

  private completeAddress() {
    this.IsLoading = true;
    this.configurationProvider.completeAddress(this.address).then(
      (response) => { 
        this.IsLoading = false;        
        this.address = response as Address;
      },
      (error) => {
        this.IsLoading = false;
        this.configurationProvider.ShowToast("Ocorreu uma falha de comunicação com o servidor, tente novamente mais tarde.")
      });
  }
  
  

}
