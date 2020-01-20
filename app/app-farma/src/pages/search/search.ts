import { SearchProvider } from './../../providers/search/search';
import { SearchResult, SearchQuery } from './../../models/search';
import { Component, ViewChild, Input } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';

@IonicPage({ name:"page-search"})
@Component({
  selector: 'page-search',
  templateUrl: 'search.html',
})
export class SearchPage {

  @ViewChild('searchInput') searchInput;

  private searchQuery: SearchQuery = new SearchQuery();
  private searchResult: SearchResult = new SearchResult();
  private searchIsLoading: boolean;
  
  constructor(public navCtrl: NavController, public navParams: NavParams, private searchProvider: SearchProvider) {
  }

  ionViewDidEnter() {
    this.doSearch();

    setTimeout(() => {
      this.searchInput.setFocus();
      
    }, 150);
  }
  
  handleSearchInputChange() {
    console.log(this.searchQuery.Query.length);
    if(this.searchQuery.Query.length == 0 || this.searchQuery.Query.length >= 3) {
      this.doSearch();
    }
  }

  doSearch() {
    this.searchIsLoading = true;
    this.searchProvider.SearchCriteria(this.searchQuery).then((result) => {
      this.searchIsLoading = false;
      this.searchResult = result;
    }).catch(()=>{
      this.searchIsLoading = false;
    });
  }  

}
