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

    constructor(public settings: SettingsService, private modalService: BsModalService) {

    }

    ngOnInit() {

    }

    public openModal(template: TemplateRef<any>) {
        this.modalRef = this.modalService.show(template);
      }

}
