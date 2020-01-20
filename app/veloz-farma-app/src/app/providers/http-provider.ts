import { Injectable } from '@angular/core';
import { Storage } from '@ionic/storage';
import { Http, RequestOptions, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs';

@Injectable()
export class HttpProvider {

  private headers: Headers = new Headers();

  constructor(private http: Http, private storage: Storage) {
  }

  public execute(endPoint: string, method: HttpMethods, payload?: any) : Observable<any> {
    
    return this.getStoredSessionKey().flatMap((storedSessionKey)=>
    {
        this.setHeader("Session", storedSessionKey);

        switch(method)
        {
            case HttpMethods.Post:
                return this.Post(endPoint, payload);

            case HttpMethods.Get:
                return this.Get(endPoint);

            case HttpMethods.Delete:
                return this.Delete(endPoint);

            case HttpMethods.Put:
                return this.Put(endPoint, payload);
        }
    });
  }

  public clearSession(): void
  {
      this.storage.set("SessionKey", "");
      this.setHeader("Session", "");
  }

  private getStoredSessionKey() : Observable<any> {
    return new Observable((observer)=>{
        this.storage.get("SessionKey").then((storedSessionKey)=>{
            observer.next(storedSessionKey);
        });
    });  
  } 

 private setHeader(headerName: string, headerValue: string) : void
  {
    if(!headerName)
        return;   
    
    if(!headerValue)
    {
        this.headers.delete(headerName);
        return
    }
        
    if(this.headers.get(headerName))
        this.headers[headerName] = headerValue;
    else
        this.headers.append(headerName, headerValue);
  }

  private getRequestConfigurations() : RequestOptions
  {
    this.setHeader("Content-Type","application/json");
    return new RequestOptions({ headers : this.headers });
  }

  private Post(endPoint: string, payload: any) : Observable<any>
  {
    return this.handleResponse(
        this.http.post(endPoint, payload, this.getRequestConfigurations()));
  }

  private Get(endPoint: string) : Observable<any>
  {
    return this.handleResponse(
        this.http.get(endPoint, this.getRequestConfigurations()));
  }
  
  private Delete(endPoint: string) : Observable<any>
  {
      return this.handleResponse(
        this.http.delete(endPoint, this.getRequestConfigurations()));
  }

  private Put(endPoint: string, payload: any) : Observable<any>
  {
      return this.handleResponse(
        this.http.put(endPoint, payload, this.getRequestConfigurations()));
  }

  private handleResponse(response: Observable<any>) : Observable<any>
  {   
    return response.map((response) => {
            let sessionKeyHeader = response.headers.get("Session");

            if( sessionKeyHeader != this.headers["Session"])
                this.storage.set("SessionKey", sessionKeyHeader);

            return response.json();
        })
        .catch((error)=>{

            if(error.status === 401)
                this.clearSession();
                
            return Observable.throw(new Error(error));
        });
  }
}

export enum HttpMethods {
    Post = 0,
    Get = 1,
    Put = 2,
    Delete = 3
}
