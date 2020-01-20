import { Component } from '@angular/core';
import { App } from 'ionic-angular';

import { OrdersPage } from '../../pages/orders/orders';
import { SessionProvider } from '../../providers/session-provider';

@Component({
  selector: 'order-fab',
  templateUrl: '/order-fab.component.html',
  styleUrls: ['/order-fab.component.scss']
})
export class OrderFabComponent {

  private showFab: boolean = false;

  constructor(private AppFarma: App, private sessionProvider: SessionProvider) {

    this.sessionProvider.sessionObservable.subscribe((result)=> {
      if (!result.AtualOrder || !result.AtualOrder.SkusRequesteds || result.AtualOrder.SkusRequesteds.length === 0)
      {
        this.showFab = false;
        return;
      }
      this.showFab = true;
    });

    this.sessionProvider.emitSessionChanges();
  }

  openOrdersPage() {
    this.AppFarma.getActiveNav().push(OrdersPage);
  }

}
