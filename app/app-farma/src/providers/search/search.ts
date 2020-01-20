import { SearchResult, SearchQuery } from './../../models/search';
import { ConfigurationProvider } from './../configuration/configuration';
import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';

@Injectable()
export class SearchProvider {

  constructor(private configurationProvider: ConfigurationProvider) {

  }

  public SearchByFulltext(term?: string) : Promise<SearchResult> {
    return this.SearchCriteria(new SearchQuery(term));
  }

  public SearchCriteria(searchQuery: SearchQuery) : Promise<SearchResult> {
    return this.configurationProvider.getRequestConfiguration().then((options) => {
      let endpoint = this.configurationProvider.getEndpointUrl("search","fulltext") + searchQuery.getQueryString();;
      return this.configurationProvider.http.get(endpoint, options).timeout(30000).map(response => response.json()).take(1).toPromise();
    });
  }

}
