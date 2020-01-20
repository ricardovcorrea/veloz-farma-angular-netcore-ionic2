import { BasePageComponent } from './../base-page.component';
import { Component } from '@angular/core';
import { NavController, NavParams, ViewController, ToastController } from 'ionic-angular';

import { SessionProvider } from '../../providers/session-provider';
import { Client, Address } from '../../models/client';
import { LoadingController } from 'ionic-angular';

import { ModalController } from 'ionic-angular';

import { AddressPage } from '../address/address';


@Component({
  templateUrl: 'location-modal.html'
})
export class LocationModal extends BasePageComponent  {
  private addressModal;
  private tempAddress : Address = new Address();

  constructor(
    public loadingProvider: LoadingController,
    public toastProvider: ToastController, 
    private modalCtrl: ModalController,
    private navCtrl: NavController, 
    private navParams: NavParams,
    private viewCtrl: ViewController,
    private sessionProvider: SessionProvider) { super(loadingProvider, toastProvider )}
  
  
  /////////////////////////////////////////////////////////////////
  //Controles da pagina
  /////////////////////////////////////////////////////////////////
  closeModal() {
    this.viewCtrl.dismiss();
  }
  /////////////////////////////////////////////////////////////////


  /////////////////////////////////////////////////////////////////
  //Botao de usar minha localização
  /////////////////////////////////////////////////////////////////  
  private useMyLocation() {
    let addressModal = this.modalCtrl.create(AddressPage, { useLocation: true });
    addressModal.present();
  }
  /////////////////////////////////////////////////////////////////  
  

  /////////////////////////////////////////////////////////////////
  //Botao de inserir endereço
  /////////////////////////////////////////////////////////////////
  private insertAddress() {
    let addressModal = this.modalCtrl.create(AddressPage);
    addressModal.present();
  }
  /////////////////////////////////////////////////////////////////
  
}
