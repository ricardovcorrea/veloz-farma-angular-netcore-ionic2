import { Component } from '@angular/core';

import { LoadingController, ToastController } from 'ionic-angular';

import { SessionProvider } from '../../providers/session-provider';
import { SearchProvider } from '../../providers/search-provider';
import { SearchQuery, SearchResult } from '../../models/search';
import { LocationModalProvider } from '../../providers/location-modal-provider';
import { BasePageComponent } from "../base-page.component";

@Component({
  selector: 'page-home',
  templateUrl: 'home.html'
})

export class HomePage {

  private homeSearchQuery: SearchQuery = new SearchQuery("Resfriado");
  private homeSearchResult: SearchResult = new SearchResult();
  private productListIsLoading: boolean = false;

  constructor(public toastProvider: ToastController, 
              private sessionProvider: SessionProvider, 
              private searchProvider: SearchProvider, 
              private locationModalProvider: LocationModalProvider) {
                this.getHomeProducts();
              }


  //Controles da pagina
  ionViewDidEnter() {
    this.getSession();
  }
  /////////////////////////////////////////////////////////////////
  

  //Restaura sessão do usuario
  private getSession() {
    this.sessionProvider.getSession().subscribe(
      (response)=> this.handleGetSessionResponse(response),
      (error) => console.log(error));
  }

  private handleGetSessionResponse(response) {
    this.sessionProvider.setSession(response);
  }

  private handleGetSessionError() {
    this.locationModalProvider.openLocationModal();
  }
  /////////////////////////////////////////////////////////////////


  //Pega os produtos da home
  async getHomeProducts() {
    try {
      this.productListIsLoading = true;
      let searchResult = await this.searchProvider.search(this.homeSearchQuery);
      if(searchResult) {
          this.homeSearchResult = searchResult;
      }
    } catch (error){ 
      let toast = this.toastProvider.create({ message: "Ocorreu uma falha de comunicação com o servidor!", duration: 3000, position: 'bottom' });
      toast.present();
    } finally {
      this.productListIsLoading = false;
    }
  } 
  /////////////////////////////////////////////////////////////////


}
