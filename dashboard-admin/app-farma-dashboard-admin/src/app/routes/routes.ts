import { AdminLayoutComponent } from './admin/admin-layout/admin-layout.component';
import { StoreLayoutComponent } from './store/store-layout/store-layout.component';
import { PublicLayoutComponent } from './public/public-layout/public-layout.component';

import { AuthenticationGuard } from "app/core/authentication/authentication.guard";

export const routes = [

    {
        path: 'public',
        component: PublicLayoutComponent,
        loadChildren: './public/public.module#PublicModule'
    },
    {
        path: 'store',
        component: StoreLayoutComponent,
        loadChildren: './store/store.module#StoreModule',
        canActivate: [AuthenticationGuard],
        data : {
            roles:[1]
        }
    },
    {
        path: 'admin',
        component: AdminLayoutComponent,
        loadChildren: './admin/admin.module#AdminModule',
        canActivate: [AuthenticationGuard],
        data : {
            roles:[-1]
        }
    },
    { path: '**', redirectTo: 'public/home' }

];
