export interface Address {
  //addressId: number;
 // restaurantId: number;
  line1: string;
  line2?: string;
  city: string;
  state: string;
  zipCode: string;
}
export interface AddressDto {
  //addressId: number;
  line1: string|null;
  line2?: string|null;
  city: string|null;
  state: string|null  ;
  zipCode: string|null  ;
  country: string|null  ;

  //restaurantId: number;
}

export interface AddressCreateDto {
  line1: string|null;
  line2?: string|null;
  city: string|null;
  state: string|null;
  zipCode: string|null  ;
  country: string|null;

  //restaurantId: number;
}

export interface AddressUpdateDto {
  //addressId: number;
  line1: string;
  line2?: string;
  city: string;
  state: string;
  zipCode: string;
  country: string;

 // restaurantId: number;
}

