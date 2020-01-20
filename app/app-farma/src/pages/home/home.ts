import { SessionProvider } from './../../providers/session/session';
import { SearchProvider } from './../../providers/search/search';
import { SearchResult } from './../../models/search';
import { ConfigurationProvider } from './../../providers/configuration/configuration';
import { Component } from '@angular/core';
import { NavController, IonicPage } from 'ionic-angular';

@IonicPage({ name:"page-home"})
@Component({
  selector: 'page-home',
  templateUrl: 'home.html'
})
export class HomePage {
  private searchResult: SearchResult = new SearchResult();
  private searchIsLoading: boolean;

  constructor(public navCtrl: NavController, private configurationProvider:ConfigurationProvider, private searchProvider: SearchProvider, sessionProvider: SessionProvider) {
    this.configurationProvider.DebugLogAction("Page home created");
    this.homeSearch();
  }

  ionViewWillEnter() {
    this.configurationProvider.DebugLogAction("Page home will enter");
    this.configurationProvider.menuController.enable(true);
  }

  // ionViewDidEnter() {
  //   this.configurationProvider.DebugLogAction("Page home entered");
  // }

  homeSearch() {
    this.searchIsLoading = true;
    
    this.searchProvider.SearchByFulltext("Tylenol").then((result) => {
      this.configurationProvider.DebugLogAction("Home search result:");
      this.configurationProvider.DebugLogAction(result);

      this.searchIsLoading = false;
      this.searchResult = result;
      
    }).catch((error)=>{
      this.configurationProvider.DebugLogAction("Home search fail:");
      this.configurationProvider.DebugLogAction(error);

      this.searchIsLoading = false;
    });
  }

  openSearch() {
    this.configurationProvider.DebugLogAction("Open Search action");

    this.navCtrl.push("page-search");
    
  }

  goToCart() {
    this.configurationProvider.DebugLogAction("Open Cart action");

    this.navCtrl.push("page-cart")
  }

  clearSession() {
    this.configurationProvider.storage.remove("Session");
    this.configurationProvider.storage.remove("IsFirstTime");
    this.configurationProvider.storage.remove("SessionToken");
    this.navCtrl.setRoot("page-tutorial");
    
  }

}
