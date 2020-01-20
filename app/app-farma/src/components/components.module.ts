import { BudgetListComponent } from './budget-list/budget-list.component';
import { IonicModule } from 'ionic-angular';
import { NgModule } from '@angular/core';
import { SearchProductListComponent } from './search-product-list/search-product-list';
import { AddressBarComponent } from './address-bar/address-bar';
import { CartButtonComponent } from './cart-button/cart-button';
import { CartProductListComponent } from './cart-product-list/cart-product-list';
import { BudgetTimerComponent } from './budget-timer/budget-timer.component';
import { CircleProgressComponent } from './circle-progress/circle-progress.component';


@NgModule({
	declarations: [SearchProductListComponent,
    AddressBarComponent,
    CartButtonComponent,
    CartProductListComponent,
    BudgetTimerComponent,
    CircleProgressComponent,
    BudgetListComponent],
	imports: [IonicModule],
	exports: [SearchProductListComponent,
    AddressBarComponent,
    CartButtonComponent,
    CartProductListComponent,
    BudgetTimerComponent,
    CircleProgressComponent,
    BudgetListComponent]
})
export class ComponentsModule {}
