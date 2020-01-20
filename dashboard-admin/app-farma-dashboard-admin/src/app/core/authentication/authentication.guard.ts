import { SettingsService } from './../settings/settings.service';
import { Injectable } from '@angular/core';
import { AuthenticationService } from './authentication.service';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

@Injectable()
export class AuthenticationGuard implements CanActivate {
   
    constructor(public authenticationService: AuthenticationService, public settingsService: SettingsService) {

    }

    public canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {  

        if(route.data.roles && route.data.roles.indexOf(-1) > -1)
            if(this.authenticationService.LoggedUser && !this.userHasRole(-1))
                this.authenticationService.forbidden();

        if (this.authenticationService.sessionNotExpired()) {
            return true;  
        }  

        this.authenticationService.logout();
        return false;  
    }

    private userHasRole(roleNumber: number) : boolean {
        return this.authenticationService.LoggedUser.roles.find((role) => {
            return role.role === roleNumber
        }) != null
    }
}
