import { Component, Input } from '@angular/core';
import { Sku } from '../../models/product'
import { SessionProvider } from '../../providers/session-provider'

@Component({
  selector: 'order-product-list',
  templateUrl: '/order-product-list.component.html',
  styleUrls: ['/order-product-list.component.scss']
})

export class OrderProductListComponent {
  @Input() products: Array<Sku>;
  @Input() isLoading: boolean;

  constructor(private sessionProvider: SessionProvider) { }

  public increaseProductQuantity(product: Sku) : void
  {
    //this.orderProvider.increaseProductQuantity(product);
  }

  public decreaseProductQuantity(product: Sku) : void
  {
    //this.orderProvider.decreaseProductQuantity(product);
  }

}
