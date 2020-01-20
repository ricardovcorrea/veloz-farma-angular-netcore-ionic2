import { BasePageComponent } from './../base-page.component';
import { App, ToastController } from 'ionic-angular';
import { Component } from '@angular/core';

import { SessionProvider } from '../../providers/session-provider';
import { LoadingController } from 'ionic-angular';

import { HomePage } from '../home/home';
import { BudgetsPage } from '../budgets/budgets';

import { Order } from '../../models/order';

@Component({
  selector: 'orders',
  templateUrl: 'orders.html'
})

export class OrdersPage extends BasePageComponent {
  private atualOrder: Order = new Order();

  constructor(loadingProvider: LoadingController, 
              toastProvider: ToastController,
              private AppFarma: App, 
              private sessionProvider: SessionProvider) { super(loadingProvider, toastProvider) }

  ionViewDidEnter()
  {
    this.showLoading().then(()=> {
      this.sessionProvider.sessionObservable.subscribe(
      (response)=> this.handleSessionResponse(response),
      (error) => this.handleSessionError(error));

    this.sessionProvider.emitSessionChanges();
    });
  }

  handleSessionResponse(response)
  {
    this.dismissLoading();

    if (!response.AtualOrder)
          return;
    
    this.atualOrder = response.AtualOrder;
  }

  handleSessionError(erro)
  {
    this.dismissLoading();
  }

  findBudgets() : void
  {
    this.AppFarma.getActiveNav().push(BudgetsPage);
  }

  back() : void
  {
    this.AppFarma.getActiveNav().pop();
  }

  goToHomePage(){
    this.AppFarma.getActiveNav().push(HomePage);
  }
}
