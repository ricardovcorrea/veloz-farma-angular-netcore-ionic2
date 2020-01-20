import { Address } from './../../models/client';
import { Session } from './../../models/session';
import { SessionProvider } from './../../providers/session/session';
import { ConfigurationProvider } from './../../providers/configuration/configuration';
import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';

@IonicPage({ name:"page-location"})
@Component({
  selector: 'page-location',
  templateUrl: 'location.html',
})
export class LocationPage {

  private CanClose: boolean;
  private IsLogged: boolean = false;
  private Session: Session = new Session();
  private IsLoading: boolean = false;
  private SessionObservable;

  constructor(public navCtrl: NavController, public navParams: NavParams, public configurationProvider: ConfigurationProvider, private sessionProvider: SessionProvider) {
    this.configurationProvider.DebugLogAction("Page location created");

    this.CanClose = this.navParams.get("CanClose") == null;

    this.configurationProvider.DebugLogAction("CanClose");
    this.configurationProvider.DebugLogAction(this.CanClose);

    this.configurationProvider.menuController.enable(false);
    
    }
  
    ionViewWillEnter() {
    this.configurationProvider.DebugLogAction("Page location entered");

    this.SessionObservable = this.sessionProvider.Session.subscribe((Session) => {
      
    this.configurationProvider.DebugLogAction("Get Session");
    this.configurationProvider.DebugLogAction(Session);
    
      this.Session = Session;
      if(this.Session && this.Session.Email) {
        this.IsLogged = true;
      } else {
        this.IsLogged = false;
      }
    });
    this.sessionProvider.EmmitSessionChanges();
  }
  
  ionViewWillLeave() {
    this.SessionObservable.unsubscribe();
  }

  address(){ 
    this.configurationProvider.DebugLogAction("Insert Addres Action");

    let addressModal = this.configurationProvider.modalController.create("page-edit-address");
    addressModal.present();
    
  }

  login() {
    this.configurationProvider.DebugLogAction("Login Action");

    this.navCtrl.push("page-login");
  }
  
  setAddress(address: Address) {
    this.configurationProvider.DebugLogAction("Set Order Adddres Action");

    this.IsLoading = true;
    this.sessionProvider.SetAddress(address).then(() => {
      this.configurationProvider.DebugLogAction("Set Order Adddres Success");

      this.IsLoading = false;

      this.navCtrl.setRoot("page-home");

    }).catch((error)=>{
      this.configurationProvider.DebugLogAction("Set Order Adddres Fail:");
      this.configurationProvider.DebugLogAction(error);

      this.IsLoading = false;
    });
  }
}
