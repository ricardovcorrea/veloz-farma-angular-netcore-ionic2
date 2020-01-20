import { Address, Client } from './client';
import  { Sku } from './product';
import  { PaymentMethodType } from './enumerables';
import  { Store } from './store';

export class Order {
    SkusRequesteds: Array<Sku>;
    AgressReference: boolean;
    AgressSimilar: boolean;
    AgressGeneric: boolean;
    Responses: Array<OrderResponse>;
    RequestedOn: Date;
    ScheduledTo: Date;
    DeliveredOn: Date;
    SelectedPayment: PaymentMethod;
    SelectedDelivery: Delivery;
    AddressToShip: Address;
    RequestedStores: Array<Store>;
    Client: Client;

    constructor()
    {
        this.SkusRequesteds = new Array<Sku>();
    }
}

export class Delivery {
    Price: number;
    MaxDistance: number;
}

export class OrderResponse {
    SkuReplys: Array<OrderResponseProduct>;
    DrugStore : Store;
    Accepted: boolean;
}

export class OrderResponseProduct {
    OriginalSku: Sku;
    OferredSku : Sku;
    Price: number;
    OutStock: boolean;
}

export class PaymentMethod {
    Type: PaymentMethodType;
}