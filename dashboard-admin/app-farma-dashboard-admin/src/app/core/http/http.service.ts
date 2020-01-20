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

  private getRequestConfigurations() : RequestOptions
  {
    let headers: Headers = new Headers();
    headers.append("Content-Type","application/json");
    
    if(this.authenticationService.LoggedUser.sessionToken)
        headers.append("Session", this.authenticationService.LoggedUser.sessionToken);    

    return new RequestOptions({ headers : headers });
  }

  public Post(endPoint: string, payload: any) : Observable<any>
  {
    return this.http.post(endPoint, payload, this.getRequestConfigurations())
  }

  public Get(endPoint: string) : Observable<any>
  {
    return this.http.get(endPoint, this.getRequestConfigurations())
  }
  
  public Delete(endPoint: string) : Observable<any>
  {
    return this.http.delete(endPoint, this.getRequestConfigurations())
  }

  public Put(endPoint: string, payload: any) : Observable<any>
  {
    return this.http.put(endPoint, payload, this.getRequestConfigurations())
  }
}