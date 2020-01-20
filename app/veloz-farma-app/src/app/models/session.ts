import { Client } from './client';
import { Order } from './order';

export class Session extends Client {
    public AtualOrder: Order;
}