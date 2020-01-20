import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';
import { LoadingController } from 'ionic-angular';
import { SessionProvider } from '../../providers/session-provider';

@Component({
  selector: 'page-budgets',
  templateUrl: 'budgets.html'
})

export class BudgetsPage {
  private loadingController;
  private budgetResponses;

  constructor(private loadingProvider: LoadingController, private sessionProvider: SessionProvider, private navCtrl: NavController) {
  }

  ionViewDidEnter()
  {
    this.sessionProvider.sessionObservable.subscribe(
      (response)=> this.handleSessionResponse(response),
      (error) => this.handleSessionError(error));

    this.sessionProvider.emitSessionChanges();
  }
  
  handleSessionResponse(response)
  {
    if (!response.Responses)
          return;

    this.budgetResponses = response.Responses;
  }

  handleSessionError(erro)
  {
    
  }

  private onTimerTick()
  {
    console.log("tick");
  }

  private onTimerFinish()
  {
    console.log("finish");
  } 

  private goBack(): void 
  {
    this.navCtrl.pop();
  }
}
