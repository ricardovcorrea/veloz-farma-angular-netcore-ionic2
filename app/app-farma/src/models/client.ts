export class Client {
  Id: number;
  Name: string;
  Email: string;
  Password: string; 
  Document: string;
  Addresses: Array<Address>;
  DeviceId: string;
  FacebookId: string;
  
  constructor()
  {
    this.Addresses = new Array<Address>();
  }
}

export class Address {
  Id:number;
  Name: string;
  PostalCode: string;
  Street: string;
  Number: number;
  Neighborhood: string;
  City: string;
  State: string;
  Complement: string;
  Reference: string;
  Latitude: string;
  Longitude: string;

  getAddressString(): string
  {
    return this.Street + ", " + this.State;
  }
}