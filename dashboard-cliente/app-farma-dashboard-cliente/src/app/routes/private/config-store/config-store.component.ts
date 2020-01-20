import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { SettingsService } from '../../../core/settings/settings.service';

@Component({
    selector: 'app-private-config-store',
    templateUrl: './config-store.component.html',
    styleUrls: ['./config-store.component.css']
})
export class ConfigStoreComponent implements OnInit {


    constructor(public settings: SettingsService) {

    }

    ngOnInit() {

    }

}
