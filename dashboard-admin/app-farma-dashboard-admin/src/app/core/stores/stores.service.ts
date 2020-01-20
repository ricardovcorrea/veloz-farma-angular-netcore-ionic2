import { PaginatedResult } from './../../shared/classes';
import { SettingsService } from './../settings/settings.service';
import { HttpService } from './../http/http.service';
import { Injectable } from '@angular/core';
import { Store } from 'app/shared/classes';

@Injectable()
export class StoresService {

    constructor(public httpService: HttpService, public settingsService: SettingsService) {

    }

    public getAllStores(page:number) : Promise<PaginatedResult>
    {   
        let endPoint = this.settingsService.getEndpointUrl("stores","list");
        endPoint += "?size=50&page="+page;
        
        return this.httpService.Get(endPoint).map(response => {
            let objResponse = response.json();
            let pagResult = new PaginatedResult(objResponse);

            if(objResponse.Result)
                objResponse.Result.forEach(store => pagResult.payload.push(new Store(store)));

            return pagResult;
        }).toPromise();
    }
}
