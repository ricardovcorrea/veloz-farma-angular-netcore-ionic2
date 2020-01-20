import { Component } from '@angular/core';
import { App } from 'ionic-angular';

import { SearchPage } from '../../pages/search/search';
import { SearchProvider } from '../../providers/search-provider';

@Component({
  selector: 'home-search-bar',
  templateUrl: '/home-search-bar.component.html',
  styleUrls: ['/home-search-bar.component.scss']
})
export class HomeSearchBarComponent {

  constructor(private AppFarma: App, private searchProvider: SearchProvider) { }

  openSearch()
  {
    this.AppFarma.getActiveNav().push(SearchPage);
  }

}
