import { AdminLayoutComponent } from './admin-layout/admin-layout.component';
import { ConfigStoreComponent } from './config-store/config-store.component';
import { DashboardComponent } from './dashboard/dashboard.component';

import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { SharedModule } from "app/shared/shared.module";

const routes: Routes = [
    { path: 'dashboard', component: DashboardComponent }];

@NgModule({
    imports: [
        SharedModule,
        RouterModule.forChild(routes)
    ],
    declarations: [
        AdminLayoutComponent,
        DashboardComponent,
        ConfigStoreComponent
        ],
    exports: [
        RouterModule
    ]
})
export class AdminModule { }
