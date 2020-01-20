import { ConfigurationProvider } from './../../providers/configuration/configuration';
import { Product } from './../../models/product';
import { SessionProvider } from './../../providers/session/session';
import { SearchProduct } from './../../models/search';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'search-product-list',
  templateUrl: 'search-product-list.html'
})
export class SearchProductListComponent {
  
  @Input() Products: Array<SearchProduct> = new Array<SearchProduct>();
  @Input() IsLoading: boolean;

  constructor(private sessionProvider: SessionProvider, private configurationProvider: ConfigurationProvider) {
    
  }

  addProductToCart(product: SearchProduct) {
    this.IsLoading = true;
    this.sessionProvider.addProductToCart(product).then(()=>{
      this.IsLoading = false;
    }).then(()=>{
      this.IsLoading = false;
    });
  }

  openProductDetail(product: SearchProduct){
    this.configurationProvider.app.getActiveNavs()[0].push("page-product-detail", { product: product });
  }

}
