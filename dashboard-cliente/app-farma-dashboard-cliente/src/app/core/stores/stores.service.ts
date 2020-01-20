import { SettingsService } from './../settings/settings.service';
import { HttpService } from './../http/http.service';
import { Injectable } from '@angular/core';

@Injectable()
export class StoresService {

    constructor(public httpService: HttpService, public settingsService: SettingsService) {

    }

    // public getAllStores() : Promise<Array<Store>>
    // {   
    //     return new Promise((resolve, reject) => {
    //     this.httpService.execute(this.settingsService.getEndpointUrl("stores","list"), HttpMethods.Get).subscribe(
    //         (response) => resolve(response),
    //         (error) => reject(error));
    //     });
    // }

    // public getStoreByScope(scope:string) : Promise<Store>
    // {   
    //     return new Promise((resolve, reject) => {
    //     this.httpService.execute(this.settingsService.getEndpointUrl("stores","detail"), HttpMethods.Get, scope ).subscribe(
    //         (response) => resolve(response),
    //         (error) => reject(error));
    //     });
    // }

    // public saveStore(store:Store) : Promise<Array<Store>>
    // {   
    //     return new Promise((resolve, reject) => {
    //     this.httpService.execute(this.settingsService.getEndpointUrl("stores","save"), HttpMethods.Post, store).subscribe(
    //         (response) => resolve(response),
    //         (error) => reject(error));
    //     });
    // }
}
