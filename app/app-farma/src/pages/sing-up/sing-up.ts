import { SessionProvider } from './../../providers/session/session';
import { Client } from './../../models/client';
import { ConfigurationProvider } from './../../providers/configuration/configuration';
import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';

/**
 * Generated class for the SingUpPage page.
 *
 * See http://ionicframework.com/docs/components/#navigation for more info
 * on Ionic pages and navigation.
 */

@IonicPage({name:"page-sing-up"})
@Component({
  selector: 'page-sing-up',
  templateUrl: 'sing-up.html',
})
export class SingUpPage {
  private Client : Client = new Client();
  private PasswordConfirmation : string;
  private IsLoading : boolean = false;

  constructor(public navCtrl: NavController, private navParams: NavParams, private configurationProvider: ConfigurationProvider, private sessionProvider: SessionProvider) {
    let clientParameter = this.navParams.get("Client");
    if(clientParameter) {
      this.Client = clientParameter;
    }
  }

  singUp() {
    if(this.Client.Password !== this.PasswordConfirmation) {
      this.configurationProvider.ShowToast("As senhas digitadas são diferentes!");
    } else {
      this.IsLoading = true;
      this.sessionProvider.CreateSession(this.Client).then(() => {
        this.IsLoading = false;
        this.configurationProvider.ShowToast("Cadastro efetuado com sucesso!");
        this.navCtrl.popToRoot();
      }).catch((error)=>{
        this.IsLoading = false;
      });
    }
  }

}
