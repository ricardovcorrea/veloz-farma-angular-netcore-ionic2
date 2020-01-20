import { Observable } from 'rxjs/Observable';
import { Http } from '@angular/http';
import { Router } from '@angular/router';
import { SettingsService } from './../settings/settings.service';
import { Injectable } from '@angular/core';
import { User } from "app/shared/classes";

import "rxjs/operator/toPromise";

@Injectable()
export class AuthenticationService {

    public LoggedUser : User;

    constructor(public http: Http, public settingsService: SettingsService, private router: Router) {

    }

    public login(email: string, password: string) : Promise<User>
    {   
        let loginEndpoint = this.settingsService.getEndpointUrl("authentication","login");
        loginEndpoint += "?email=" + email + "&password=" + password;

        return this.http.get(loginEndpoint).map((response) => {
            this.LoggedUser = new User(response.json());
            
            let sessionToken = response.headers.get("session");
            if(sessionToken){ 
                this.LoggedUser.sessionToken = sessionToken;
            }

            let sessionExpires = response.headers.get("ttl");
            if(sessionExpires){ 
                this.LoggedUser.sessionExpire = sessionExpires;
            }

            localStorage.setItem("LoggedUser", JSON.stringify(this.LoggedUser));

            return this.LoggedUser;

        }).toPromise();
    }

    public logout()
    {   
        localStorage.removeItem("LoggedUser");
        this.router.navigate(['public/login']);
    }

    public sessionNotExpired() : boolean
    {
        let stringLoggedUser = localStorage.getItem("LoggedUser");
        if(stringLoggedUser) {
            this.LoggedUser = JSON.parse(stringLoggedUser);
            if(new Date(this.LoggedUser.sessionExpire) > new Date())
                return true;
        } else {
            this.logout();
            return false;
        }
    }
}
