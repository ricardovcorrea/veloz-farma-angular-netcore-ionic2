import { Router } from '@angular/router';
import { environment } from './../../../environments/environment';
import { Injectable } from '@angular/core';

@Injectable()
export class SettingsService {

    public user: any;
    public app: any;
    public layout: any;
    public environment:any;
    public apiEndpoints:any = {
        authentication: {
            login:"user/login",
            create:"user"
        },
        stores: {
            list: "store",
            detail: "api/stores",
            save: "api/stores"
        }
    };

    constructor(public router: Router) {
        
        this.environment = environment;
    
        this.app = {
            name: 'APP FARMA',
            description: 'APP FARMA',
            year: ((new Date()).getFullYear())
        };

    }

    public getEndpointUrl(context: string, action: string) : string
    {   
        let endpoint = this.apiEndpoints[context][action];
        if(endpoint) {
            return  this.getBaseUrl() + endpoint;
        }
        else {
            console.log("Cannot get endpoint!");
            console.log("Context: "+context);
            console.log("Context: "+context);
            return "";
        }
    }

    private getBaseUrl() : string
    {
        return this.environment.apiBaseProtocol + "://" +this.environment.apiBaseUrl + ":" + this.environment.apiBasePort +"/";
    }

}

