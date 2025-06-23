import { IAddress } from "./address";
/*
///////// ده الرد اللي هييجي من ال api 
//////// ف لازم عناصر اللي هنا تبقي زيه ب الظبط !
[
  {
    "id": 0,
    "clientEmail": "string",
    "orderDate": "2025-06-19T18:39:51.512Z",
    "orderState": "string",
    "shippingAddress": {
      "firstName": "string",
      "lastName": "string",
      "street": "string",
      "city": "string",
      "country": "string"
    },
    "deliveryMethodCost": 0,
    "items": [
      {
        "productId": 0,
        "productName": "string",
        "pictureUrl": "string",
        "price": 0,
        "quantity": 0
      }
    ],
    "subTotal": 0,
    "total": 0
  }
]

*/ 
export interface IOrderToCreate {
    basketId: string;
    //deliveryMethodId: number;
    shippingAddress: IAddress;
}

export interface IOrder {
    id: number;
    clientEmail: string;
    orderDate: string;
    shippingAddress: IAddress;
    deliveryMethodCost: number;
    items: IOrderItem[];
    subTotal: number;
    orderState: string;
    total: number;
  }
  
  export interface IOrderItem {
    productId: number;
    productName: string;
    pictureUrl: string;
    price: number;
    quantity: number;
  }