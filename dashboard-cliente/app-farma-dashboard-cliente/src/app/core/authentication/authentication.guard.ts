import { SettingsService } from './../settings/settings.service';
import { Injectable } from '@angular/core';
import { AuthenticationService } from './authentication.service';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

@Injectable()
export class AuthenticationGuard implements CanActivate {
   
    constructor(public authenticationService: AuthenticationService, public settingsService: SettingsService) {

    }

    public canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {  
        if (this.authenticationService.sessionNotExpired()) {
            return true;  
        }  

        this.settingsService.router.navigate(['public/login']);  
        return false;  
    }
}
