import { Observable } from 'rxjs/Observable';
import { Http } from '@angular/http';
import { Router } from '@angular/router';
import { SettingsService } from './../settings/settings.service';
import { Injectable } from '@angular/core';
import { User } from "app/shared/classes";

import "rxjs/operator/toPromise";
import { GenericError } from 'app/shared/generic-error';

@Injectable()
export class AuthenticationService {

    public LoggedUser : User;

    constructor(public http: Http, public settingsService: SettingsService, private router: Router) {
        this.sessionNotExpired();
    }

    public login(email: string, password: string) : Promise<User> {   
        let loginEndpoint = this.settingsService.getEndpointUrl("authentication","login");
        loginEndpoint += "?email=" + email + "&password=" + password;

        return this.http.get(loginEndpoint).map((response) => {
            if(response.status === 200) {
                let sessionToken = response.headers.get("session");
                let sessionExpires = response.headers.get("ttl");
                
                if(!sessionToken)
                    throw new GenericError(500); 
                
                if(!sessionExpires)
                    throw new GenericError(500);
                
                this.LoggedUser = new User(response.json());
                this.LoggedUser.sessionToken = sessionToken;
                this.LoggedUser.sessionExpire = sessionExpires;

                localStorage.setItem("LoggedUser", JSON.stringify(this.LoggedUser));
                
                return this.LoggedUser;

            } 
            else if (response.status === 401) throw new GenericError(401);
            else throw new GenericError(500);

        }).toPromise();
    }

    public logout() {   
        localStorage.removeItem("LoggedUser");
        this.router.navigate(['public/login']);
    }

    public forbidden() {   
        this.router.navigate(['public/forbidden']);
    }

    public sessionNotExpired() : boolean {
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
