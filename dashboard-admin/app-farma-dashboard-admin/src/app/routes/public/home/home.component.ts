import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { SettingsService } from '../../../core/settings/settings.service';

@Component({
    selector: 'app-public-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {


    constructor(public settings: SettingsService) {

    }

    ngOnInit() {

    }

}
