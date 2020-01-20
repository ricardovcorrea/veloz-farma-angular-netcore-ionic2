import { ConfigurationProvider } from './../../providers/configuration/configuration';
import { SessionProvider } from './../../providers/session/session';
import { Client } from './../../models/client';
import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';

@IonicPage({name:"page-login"})
@Component({
  selector: 'page-login',
  templateUrl: 'login.html',
})
export class LoginPage {
  public Credentials: Client = new Client();
  private IsLoading: boolean = false;

  constructor(public navCtrl: NavController, public navParams: NavParams, public configurationProvider: ConfigurationProvider,public sessionProvider: SessionProvider) {
    this.Credentials.Email = "mimimi@email.com.br";
    this.Credentials.Password = "1234";

    this.configurationProvider.facebookProvider.browserInit(1119399908191369, "v2.8");
  }

  login() {
    this.IsLoading = true;
    this.sessionProvider.Login(this.Credentials).then(()=>{
      this.IsLoading = false;
      this.navCtrl.pop();
    }).catch((error)=>{
      this.IsLoading = false;
      if(error.status === 401) {
        this.configurationProvider.ShowToast("Usuário e/ou senha invalidos!");
      } else {
        this.configurationProvider.ShowToast("Ocorreu uma falha de comunicação com o sevidor!");
      }
    });
  }

  facebookLogin(){

  }

  singUp() {
    this.navCtrl.push("page-sing-up");
  }

  forgetPassword() {
    this.navCtrl.push("page-forget-password");
  }

  doFbLogin(){
    let newClient = new Client();
    this.configurationProvider.facebookProvider.login(["public_profile","email"]).then((FacebookResponse)=> {
      newClient.FacebookId = FacebookResponse.authResponse.userID;
      this.configurationProvider.facebookProvider.api("/me?fields=name,email", new Array<string>()).then((User) => {
        newClient.Email = User.email;
        newClient.Name = User.name;
        this.navCtrl.push("page-sing-up",{ Client : newClient});
      });
    }).catch((error)=>{
      this.configurationProvider.ShowToast("Ocorreu uma falha de comunicação!");
      console.error(error);
    });
  }

}
