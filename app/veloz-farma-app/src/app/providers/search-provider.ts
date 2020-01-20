import { Injectable } from '@angular/core';
import { ModalController } from 'ionic-angular';

import { SearchQuery, SearchResult } from '../models/search';

import { ConfigurationProvider } from './configuration-provider';
import { HttpProvider, HttpMethods } from './http-provider';

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/toPromise';

@Injectable()
export class SearchProvider {

  constructor(public modalCtrl: ModalController, private httpProvider: HttpProvider, public configurationProvider: ConfigurationProvider) {

  }

  public async search(query: SearchQuery) : Promise<SearchResult> {

    let searchUrl = this.configurationProvider.getEndpointUrl("search", "fulltext") + query.getQueryString();
    return new Promise<SearchResult>((resolve, reject) => {
      this.httpProvider.execute(searchUrl, HttpMethods.Get).subscribe((res)=> resolve(res),(err) => reject(err))});
  }

}
