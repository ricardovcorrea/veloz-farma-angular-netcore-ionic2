import { AuthenticationService } from './../../../core/authentication/authentication.service';
import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { SettingsService } from '../../../core/settings/settings.service';

@Component({
    selector: 'app-store-layout',
    templateUrl: './store-layout.component.html',
    styleUrls: ['./store-layout.component.css']
})
export class StoreLayoutComponent implements OnInit {


    constructor(public authenticationService: AuthenticationService) {

    }

    ngOnInit() {

    }

    logout() {
        this.authenticationService.logout();
    }

}
