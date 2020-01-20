import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { PaymentPinPage } from './payment-pin';

@NgModule({
  declarations: [
    PaymentPinPage,
  ],
  imports: [
    IonicPageModule.forChild(PaymentPinPage),
  ],
  exports: [
    PaymentPinPage
  ]
})
export class PaymentPinPageModule {}
