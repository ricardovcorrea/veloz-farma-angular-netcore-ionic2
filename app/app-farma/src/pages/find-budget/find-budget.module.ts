import { ComponentsModule } from './../../components/components.module';
import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { FindBudgetPage } from './find-budget';

@NgModule({
  declarations: [
    FindBudgetPage,
  ],
  imports: [
    IonicPageModule.forChild(FindBudgetPage),
    ComponentsModule
  ],
})
export class FindBudgetPageModule {}
