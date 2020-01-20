import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { MyBudgetsPage } from './my-budgets';

@NgModule({
  declarations: [
    MyBudgetsPage,
  ],
  imports: [
    IonicPageModule.forChild(MyBudgetsPage),
  ],
})
export class MyBudgetsPageModule {}
