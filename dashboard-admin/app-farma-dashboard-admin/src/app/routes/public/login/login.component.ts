import { AuthenticationService } from './../../../core/authentication/authentication.service';
import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { SettingsService } from '../../../core/settings/settings.service';

@Component({
    selector: 'app-public-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {


    constructor(public settings: SettingsService, public authenticationService: AuthenticationService) {

    }

    ngOnInit() {

    }

    login(){
        this.authenticationService.login("contato@ihchegou.com.br","123qweQWE").then((user) => {
            this.settings.router.navigate(['private/dashboard']);
        }).catch((error)=>{
            alert(error.message);
        });
    }

    session(){
        this.authenticationService.sessionNotExpired();
    }

    logout() {
        this.authenticationService.logout();
    }
}
