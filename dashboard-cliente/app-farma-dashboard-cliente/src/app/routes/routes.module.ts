import { PrivateModule } from './private/private.module';
import { PublicModule } from './public/public.module';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MenuService } from '../core/menu/menu.service';
import { SharedModule } from '../shared/shared.module';

import { menu } from './menu';
import { routes } from './routes';

@NgModule({
    imports: [
        SharedModule,
        PublicModule,
        PrivateModule,
        RouterModule.forRoot(routes)
    ],
    declarations: [],
    exports: [
        RouterModule
    ]
})

export class RoutesModule {
    constructor(public menuService: MenuService) {
        menuService.addMenu(menu);
    }
}
