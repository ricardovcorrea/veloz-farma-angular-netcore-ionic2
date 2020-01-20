import { ConfigStoreComponent } from './config-store/config-store.component';
import { StoreLayoutComponent } from './store-layout/store-layout.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { SharedModule } from "app/shared/shared.module";

const routes: Routes = [
    { path: 'dashboard', component: DashboardComponent },
    { path: 'configuration', component: ConfigStoreComponent }
];

@NgModule({
    imports: [
        SharedModule,
        RouterModule.forChild(routes)
    ],
    declarations: [
        StoreLayoutComponent,
        DashboardComponent,
        ConfigStoreComponent
        ],
    exports: [
        RouterModule
    ]
})
export class StoreModule { }
