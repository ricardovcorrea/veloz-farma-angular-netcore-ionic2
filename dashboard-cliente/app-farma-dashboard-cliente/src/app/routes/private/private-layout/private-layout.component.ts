import { AuthenticationService } from './../../../core/authentication/authentication.service';
import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { SettingsService } from '../../../core/settings/settings.service';

@Component({
    selector: 'app-home',
    templateUrl: './private-layout.component.html',
    styleUrls: ['./private-layout.component.css']
})
export class PrivateLayoutComponent implements OnInit {


    constructor(public authenticationService: AuthenticationService) {

    }

    ngOnInit() {

    }

    logout() {
        this.authenticationService.logout();
    }

}
