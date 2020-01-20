import { PrivateLayoutComponent } from './private-layout/private-layout.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { SharedModule } from "app/shared/shared.module";
import { ConfigStoreComponent } from 'app/routes/private/config-store/config-store.component';

const routes: Routes = [
    { path: 'dashboard', component: DashboardComponent },
    { path: 'store/config', component: ConfigStoreComponent }
];

@NgModule({
    imports: [
        SharedModule,
        RouterModule.forChild(routes)
    ],
    declarations: [
        PrivateLayoutComponent,
        DashboardComponent,
        ConfigStoreComponent
        ],
    exports: [
        RouterModule
    ]
})
export class PrivateModule { }
