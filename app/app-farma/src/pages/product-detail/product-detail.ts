import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';


@IonicPage({ name:"page-product-detail"})
@Component({
  selector: 'page-product-detail',
  templateUrl: 'product-detail.html',
})
export class ProductDetailPage {

  constructor(public navCtrl: NavController, public navParams: NavParams) {
  }

  ionViewDidLoad() {
    console.log('ionViewDidLoad ProductDetailPage');
  }
  
  items = [
    'Para que serve',
    'Contraindicação',
    'Como usar',
    'Precauções',
    'Reações Adversas',
    'Composição',
    'Superdosagem',
    'Interação Medicamentosa',
    'Interação Alimentícia',
    'Ação da Substância',
    'Cuidados de Armazenamento',
    'Dizeres Legais',
  ];

  itemSelected(item: string) {
    console.log("Selected Item", item);
  }

}
