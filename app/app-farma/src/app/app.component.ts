import { SessionProvider } from './../providers/session/session';
import { ConfigurationProvider } from './../providers/configuration/configuration';
import { Component, ViewChild } from '@angular/core';
import { Nav, Platform } from 'ionic-angular';
import { StatusBar } from '@ionic-native/status-bar';
import { SplashScreen } from '@ionic-native/splash-screen';

@Component({
  templateUrl: 'app.html'
})
export class MyApp {
  @ViewChild(Nav) nav: Nav;

  rootPage: any;

  pages: Array<{title: string, component: string, icon: string, isRoot?:boolean}>;

  constructor(public platform: Platform, public statusBar: StatusBar, public splashScreen: SplashScreen, private configurationProvider: ConfigurationProvider, private sessionProvider:SessionProvider) {
    this.pages = [
      { title: 'Home', component: "page-home", icon: "ios-home-outline", isRoot: true},
      { title: 'Categorias', component: "page-categories", icon: "ios-list-outline" },
      { title: 'Meu Perfil', component: "page-profile", icon: "ios-person-outline"},
      { title: 'Meus orçamentos', component: "", icon: "ios-timer-outline" },
      { title: 'Fale conosco', component: "", icon: "ios-contacts-outline" }
    ];

    this.initializeApp();
  }

  initializeApp() {

    this.configurationProvider.DebugLogAction("App Initialize");

    this.platform.ready().then(() => {
      this.statusBar.styleDefault();

      try{
        if(this.configurationProvider.platform.is("cordova")) {
          this.configurationProvider.oneSignal.startInit('ae241884-4424-415d-b1c1-5f504448304a', '953530480062');
          this.configurationProvider.oneSignal.endInit();
        }
      }
      catch(error) {
        alert(JSON.stringify(error));
      }
      

      this.configurationProvider.storage.get("IsFirstTime").then((IsFirstTime) => {
        if(!IsFirstTime) {

          this.configurationProvider.DebugLogAction("First Time Open");

          this.configurationProvider.storage.set("IsFirstTime", true);
          this.splashScreen.hide();            
          this.nav.setRoot("page-tutorial");
        } else {

          this.configurationProvider.DebugLogAction("Not First Time Open");

          this.sessionProvider.LoadSession().then((Session) => {
            if(Session && Session.AtualOrder && Session.AtualOrder.AddressToShip) {

              this.configurationProvider.DebugLogAction("Session, Order and Address");

              this.splashScreen.hide();            
              this.nav.setRoot("page-home");

            } else {

              this.configurationProvider.DebugLogAction("No Session or no Order or no Address");

              this.splashScreen.hide();            
              this.nav.setRoot("page-location", { CanClose: false } );
            }
          });
        }
      });
    });

  }

  openPage(page: any) {
    if(!page.component)
      return;
    
    if(page.isRoot) {
      this.nav.setRoot(page.component);
    } else {
      this.nav.push(page.component);
    }
  }
}
