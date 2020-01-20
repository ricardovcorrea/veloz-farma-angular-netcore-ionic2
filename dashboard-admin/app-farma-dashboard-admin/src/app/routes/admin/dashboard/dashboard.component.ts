import { Store } from 'app/shared/classes';
import { StoresService } from './../../../core/stores/stores.service';
import { Router } from '@angular/router';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { SettingsService } from '../../../core/settings/settings.service';

import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/modal-options.class';

@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
    public modalRef: BsModalRef;
    public stores: Array<Store> = new Array<Store>();
    public totalPages: number = 0;
    public currentPage: number = 0;

    constructor(public settings: SettingsService, private modalService: BsModalService, private StoresService: StoresService) {

    }

    ngOnInit() {
        this.getStores();
    }

    public getStores() {
        this.StoresService.getAllStores(this.currentPage).then((data)=>{
            this.stores = data.payload;
            this.totalPages = data.totalPages;
            this.currentPage = data.currentPage;
        });
    }

    public openModal(template: TemplateRef<any>) {
        this.modalRef = this.modalService.show(template);
      }

}
