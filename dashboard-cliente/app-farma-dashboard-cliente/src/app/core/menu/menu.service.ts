import { Injectable } from '@angular/core';
import { Subject } from "rxjs/Subject";

@Injectable()
export class MenuService {
    public menuObservable = new Subject<Array<any>>();
    public menuActionsBroadcaster = new Subject<string>();

    menuItems: Array<any>;
    
    constructor() {
        this.menuItems = [];
    }

    addMenu(items: Array<{
        text: string,
        heading?: boolean,
        link?: string,     // internal route links
        elink?: string,    // used only for external links
        target?: string,   // anchor target="_blank|_self|_parent|_top|framename"
        icon?: string,
        alert?: string,
        submenu?: Array<any>,
        active?: boolean
    }>) {
        
        this.menuItems = [];

        items.forEach((item) => {
            if(item.active)
                this.menuItems.push(item);
                
        });
        
        this.menuObservable.next(this.menuItems);
    }

    getMenu() {
        return this.menuItems;
    }

    broadCastMenuAction(action: string)
    {
        this.menuActionsBroadcaster.next(action);
    }

}
