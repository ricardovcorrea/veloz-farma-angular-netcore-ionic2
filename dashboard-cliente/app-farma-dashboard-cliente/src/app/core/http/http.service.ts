import { AuthenticationService } from './../authentication/authentication.service';
import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { Http, RequestOptions, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs';

@Injectable()
export class HttpService {

  constructor(private http: Http, private authenticationService: AuthenticationService) {
  }

  private getRequestConfigurations(isAuthRequest? : boolean) : RequestOptions
  {
    let headers: Headers = new Headers();
    headers.append("Content-Type","application/json");
    
    let loggedUserToken = this.authenticationService.LoggedUser.sessionToken;
    if(loggedUserToken)
        headers.append("Session", loggedUserToken);    

    return new RequestOptions({ headers : headers });
  }

  private Post(endPoint: string, payload: any) : Observable<any>
  {
    if(this.authenticationService.sessionNotExpired)
        return this.handleResponse(this.http.post(endPoint, payload, this.getRequestConfigurations()))
  }

  private Get(endPoint: string, payload?: any) : Observable<any>
  {
    if(payload)
        endPoint += "/"+payload;

    if(this.authenticationService.sessionNotExpired)
        return this.handleResponse(this.http.get(endPoint, this.getRequestConfigurations()))
    
  }
  
  private Delete(endPoint: string) : Observable<any>
  {
    if(this.authenticationService.sessionNotExpired)
        return this.handleResponse(this.http.delete(endPoint, this.getRequestConfigurations()))
    
  }

  private Put(endPoint: string, payload: any) : Observable<any>
  {
    if(this.authenticationService.sessionNotExpired)
        return this.handleResponse(this.http.put(endPoint, payload, this.getRequestConfigurations()))
   
  }

  private handleResponse(response: Observable<any>) : Observable<any>
  {   
    return response.map((response) => {
            if(response.Code === 401){
                this.authenticationService.logout();
                return;
            }

            return response.json();
        })
        .catch((error)=>{
            return Observable.throw(new Error(error));
        });
  }
}