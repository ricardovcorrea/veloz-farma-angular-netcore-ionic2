import { Injectable } from '@angular/core';
import { ModalController } from 'ionic-angular';
import { LocationModal } from '../pages/location-modal/location-modal';

@Injectable()
export class LocationModalProvider {

  constructor(public modalCtrl: ModalController) {

  }
  public openLocationModal() : void
  {
    let locationModal = this.modalCtrl.create(LocationModal);
    locationModal.present();
  }

}
