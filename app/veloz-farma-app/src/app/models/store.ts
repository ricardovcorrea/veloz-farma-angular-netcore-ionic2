import { Address } from './client';
import { PaymentMethod, Delivery } from './order';

export class Store {
    Name: string;
    Email: string;
    Password: string;
    Address: Address;
    AvailablePayments: Array<PaymentMethod>;
    AvaibleDeliveries: Array<Delivery>;
    Proximity: number;
    MaxDistance: number;
}