export class SearchResult {
    QueryTime: string;
    Request: SearchQuery;
    Result: Array<SearchProduct>;
}

export class SearchProduct {
    ProductId: number;
    Product: string;
    Serving: string;
    Image: string;
    SkuName: string;
    SkuId: number;
    Producer: string;    
    Categories : string;
    Principles : string;
}

export class SearchQuery {
    Query: string;
    From: number = 0;
    Take: number = 10;
    
    constructor(query?: string)
    {
        this.Query = query || "";
    }
    
    getQueryString()
    {
        return "?Query=" + this.Query + "&From=" + this.From + "&Take=" + this.Take;
    }
}