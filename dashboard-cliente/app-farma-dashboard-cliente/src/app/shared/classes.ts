import * as enums from './enum';

export class User {
    
    constructor(json_user){
        this.id = json_user.Id;
        this.email = json_user.Email;
        this.name = json_user.Name;
    }

    id:string;
    email:string;
    password:string;
    name:string;
    
    sessionToken:string;
    sessionExpire:string;
}