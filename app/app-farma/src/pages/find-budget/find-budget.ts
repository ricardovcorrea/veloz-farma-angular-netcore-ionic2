import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';

/**
 * Generated class for the FindBudgetPage page.
 *
 * See http://ionicframework.com/docs/components/#navigation for more info
 * on Ionic pages and navigation.
 */

@IonicPage({name:'page-find-budget'})
@Component({
  selector: 'page-find-budget',
  templateUrl: 'find-budget.html',
})
export class FindBudgetPage {

  constructor(public navCtrl: NavController, public navParams: NavParams) {
  }

  ionViewDidLoad() {
    console.log('ionViewDidLoad FindBudgetPage');
  }

  onTimerTick(){
    console.log("TICK");
  }


  onTimerFinish(){
    console.log("FINISHED");
  }

}
