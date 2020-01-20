import { PrivateLayoutComponent } from './private/private-layout/private-layout.component';
import { PublicLayoutComponent } from './public/public-layout/public-layout.component';

import { AuthenticationGuard } from "app/core/authentication/authentication.guard";

export const routes = [

    {
        path: 'public',
        component: PublicLayoutComponent,
        loadChildren: './public/public.module#PublicModule'
    },
    {
        path: 'private',
        component: PrivateLayoutComponent,
        loadChildren: './private/private.module#PrivateModule',
        canActivate: [AuthenticationGuard]
    },
    { path: '**', redirectTo: 'public/home' }

];
