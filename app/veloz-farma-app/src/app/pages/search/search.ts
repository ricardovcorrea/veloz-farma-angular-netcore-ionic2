import { Component } from '@angular/core';
import { NavController, ViewController } from 'ionic-angular';

import { SearchQuery, SearchResult } from '../../models/search';
import { SearchProvider } from '../../providers/search-provider';

@Component({
  templateUrl: 'search.html'
})
export class SearchPage {
  private searchQuery: SearchQuery = new SearchQuery();
  private searchResult: SearchResult = new SearchResult();
  
  constructor(private viewCtrl: ViewController, private searchProvider: SearchProvider) {
    this.doSearch();
  }

  async doSearch(newValue?)
  {
    this.searchQuery.Query = newValue || this.searchQuery.Query;
    this.searchResult = await this.searchProvider.search(this.searchQuery);
  }

  clear()
  {
    this.searchQuery.Query = "";
  }

  handleSearchResult(result: SearchResult)
  {
    this.searchResult = result;
  }

  closeModal()
  {
    this.viewCtrl.dismiss();
  }

}
