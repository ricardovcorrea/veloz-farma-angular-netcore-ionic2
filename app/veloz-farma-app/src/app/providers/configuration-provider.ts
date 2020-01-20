import { Injectable } from '@angular/core';

@Injectable()
export class ConfigurationProvider {

  private apiBaseProtocol: string = "http";
  private apiBaseUrl: string = "ihchegou.mobfiq.com.br";
  private apiBasePort: number = 80;
  private apiEndpoints = {
    general: {
      completeAddress:"api/pub/address/info"
    },
    search: {
      fulltext: "api/pub/fulltext"
    },
    client: {
      create: "api/pub/client",
      get: "api/pub/client"
    },
    order:{
      addAddress: "api/pub/order/address",
      addProduct: "api/pub/order/sku"
    }
  }

  constructor() {
    
  }

  public getEndpointUrl(context: string, action: string) : string
  {
    return this.apiEndpoints[context][action] ? this.getBaseUrl() + this.apiEndpoints[context][action] : '';
  }

  private getBaseUrl() : string
  {
    return this.apiBaseProtocol + "://" +this.apiBaseUrl + ":" + this.apiBasePort +"/";
  }

}
