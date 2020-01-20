import { AuthenticationService } from './../../../core/authentication/authentication.service';
import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { SettingsService } from '../../../core/settings/settings.service';

@Component({
    selector: 'app-admin-layout',
    templateUrl: './admin-layout.component.html',
    styleUrls: ['./admin-layout.component.css']
})
export class AdminLayoutComponent implements OnInit {


    constructor(public authenticationService: AuthenticationService) {

    }

    ngOnInit() {

    }

    logout() {
        this.authenticationService.logout();
    }

}
