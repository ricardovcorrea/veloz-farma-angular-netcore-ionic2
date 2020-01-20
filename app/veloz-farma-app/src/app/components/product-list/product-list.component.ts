import { Component, Input } from '@angular/core';
import { SearchProduct } from '../../models/search'

import { SessionProvider } from '../../providers/session-provider';

@Component({
  selector: 'product-list',
  templateUrl: '/product-list.component.html',
  styleUrls: ['/product-list.component.scss']
})

export class ProductListComponent {
  @Input() products: Array<SearchProduct> = new Array<SearchProduct>();
  @Input() loading: boolean;  
  
  constructor(public sessionProvider: SessionProvider) {}
  
  public addToCart(product: SearchProduct)
  {
    this.sessionProvider.addProductToOrder(product);
  }

}
