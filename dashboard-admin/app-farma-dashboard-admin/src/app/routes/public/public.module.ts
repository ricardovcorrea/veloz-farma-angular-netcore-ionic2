import { ForbiddenComponent } from './forbidden/forbidden.component';
import { PublicLayoutComponent } from './public-layout/public-layout.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { SharedModule } from "app/shared/shared.module";
import { LoginComponent } from 'app/routes/public/login/login.component';

const routes: Routes = [
    { path: 'home', component: HomeComponent },
    { path: 'login', component: LoginComponent },
    { path: 'forbidden', component: ForbiddenComponent }

];

@NgModule({
    imports: [
        SharedModule,
        RouterModule.forChild(routes)
    ],
    declarations: [
        PublicLayoutComponent,
        HomeComponent,
        LoginComponent,
        ForbiddenComponent
        ],
    exports: [
        RouterModule
    ]
})
export class PublicModule { }
