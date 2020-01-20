import * as enums from './enum';

export class User {
    
    constructor(json_user){
        this.id = json_user.Id;
        this.email = json_user.Email;
        this.name = json_user.Name;
        this.roles = new Array<UserRole>();

        if(json_user.Roles)
            json_user.Roles.forEach(role => this.roles.push(new UserRole(role)));  
    }

    id:string;
    email:string;
    password:string;
    name:string;
    roles:Array<UserRole>;
    
    sessionToken:string;
    sessionExpire:string;
}

export class UserRole {
    
    constructor(json_role) {
        this.role = json_role.Role;
        this.store = json_role.Store;
    }

    role:number;
    store:any;
}

export class Store {

    constructor(json_store){
        this.id = json_store.Id;
        this.name = json_store.Name;
    }

    id:string;
    name:string;
}

export class PaginatedResult {

    constructor(json_paginated_result){
        this.totalPages = json_paginated_result.TotalPages;
        this.currentPage = json_paginated_result.CurrentPage;
        this.payload = new Array<any>();
    }

    totalPages:number;
    currentPage:number;
    payload:Array<any>;
}