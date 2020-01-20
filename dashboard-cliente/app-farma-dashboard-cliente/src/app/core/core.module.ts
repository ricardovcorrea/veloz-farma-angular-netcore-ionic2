import { StoresService } from './stores/stores.service';
import { HttpService } from './http/http.service';
import { AuthenticationService } from './authentication/authentication.service';
import { AuthenticationGuard } from './authentication/authentication.guard';
import { NgModule, Optional, SkipSelf } from '@angular/core';

import { SettingsService } from './settings/settings.service';
import { MenuService } from './menu/menu.service';

import { throwIfAlreadyLoaded } from './module-import-guard';

@NgModule({
    imports: [
    ],
    providers: [
        SettingsService,
        MenuService,
        HttpService,
        AuthenticationGuard,
        AuthenticationService,
        StoresService
    ],
    declarations: [
    ],
    exports: [
    ]
})
export class CoreModule {
    constructor( @Optional() @SkipSelf() parentModule: CoreModule) {
        throwIfAlreadyLoaded(parentModule, 'CoreModule');
    }
}
