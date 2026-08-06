export interface Address {
  addressId: number;
  restaurantId: number;
  line1: string;
  line2?: string;
  city: string;
  state: string;
  zipCode: string;
}
export interface AddressDto {
  addressId: number;
  restaurantId: number;
  line1: string;
  line2?: string;
  city: string;
  state: string;
  zipCode: string;
}
