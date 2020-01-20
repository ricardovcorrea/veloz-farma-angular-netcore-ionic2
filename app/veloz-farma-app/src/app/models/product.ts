import { SearchProduct } from './search';

export class Product {
  Id: number;
  Name: string;
  Serving: string;
  Type: number;
  NeedRecipe: boolean;
  Categories: Array<Category>;
  Producer: Producer;
  Principles: Array<Principle>;
  Skus : Array<Sku>;
  Views: number;
}

export class Sku {
    Id: number;
    Name: string;
    Image: string;
    ProductReference: Product;
    OrderSkuId: string;
    Quantity: number;

    constructor(searchProduct: SearchProduct)
    {
        this.Id = searchProduct.SkuId;
        this.Quantity = 1;
    }
}

export class Category {
    Name: string;
    SubCategories: Array<Category>;
}

export class Producer {
    Name: string;
}

export class Principle {
    Name: string;
}