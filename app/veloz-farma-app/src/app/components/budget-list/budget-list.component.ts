import { Component, Input } from '@angular/core';
import { OrderResponse } from '../../models/order'

@Component({
  selector: 'budget-list',
  templateUrl: '/budget-list.component.html',
  styleUrls: ['/budget-list.component.scss']
})

export class BudgetListComponent {
  @Input() budgets: Array<OrderResponse> = new Array<OrderResponse>();

  constructor() {}

}
