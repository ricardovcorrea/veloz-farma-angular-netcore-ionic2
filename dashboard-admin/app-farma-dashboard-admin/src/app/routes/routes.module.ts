import { StoreModule } from './store/store.module';
import { AdminModule } from './admin/admin.module';
import { PublicModule } from './public/public.module';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';

import { routes } from './routes';

@NgModule({
    imports: [
        SharedModule,
        PublicModule,
        StoreModule,
        AdminModule,
        RouterModule.forRoot(routes)
    ],
    declarations: [],
    exports: [
        RouterModule
    ]
})

export class RoutesModule {
    constructor() {
    }
}
