import { Injectable} from '@angular/core';
import { ToastController } from 'ionic-angular';

import { Sku } from '../models/sku'; 

import 'rxjs/add/operator/map';
import { Subject } from 'rxjs/Subject'

@Injectable()
export class OrderProvider {
  private cart: any;
  public cartObservable = new Subject<any>();

  constructor(public toastController: ToastController) {
  }
}
