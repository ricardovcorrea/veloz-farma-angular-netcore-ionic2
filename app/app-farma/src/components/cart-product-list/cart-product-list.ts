import { SessionProvider } from './../../providers/session/session';
import { Sku } from './../../models/product';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'cart-product-list',
  templateUrl: 'cart-product-list.html'
})
export class CartProductListComponent {

  private CanChangeQuantity: boolean = true;

  @Input() Skus: Array<Sku> = new Array<Sku>();
  @Input() IsLoading: boolean;
  
  constructor(private sessionProvider:SessionProvider) {
    
  }

  increaseQuantity(skuToIncrease : Sku) {
    if(!this.CanChangeQuantity)
      return;

    this.CanChangeQuantity = false;
    skuToIncrease.Quantity += 1;
    this.sessionProvider.addSkuToCart(skuToIncrease).then(()=>{
      this.CanChangeQuantity = true;
    }).catch(()=>{
      this.CanChangeQuantity = true;
    });
     
  }

  decreaseQuantity(skuToIncrease : Sku) {
    if(!this.CanChangeQuantity)
    return;
    
    if(skuToIncrease.Quantity <= 1)
    return;
    
    this.CanChangeQuantity = false;
    skuToIncrease.Quantity -= 1;
    this.sessionProvider.addSkuToCart(skuToIncrease).then(()=>{
      this.CanChangeQuantity = true;
    }).catch(()=>{
      this.CanChangeQuantity = true;
    });
  }

}
