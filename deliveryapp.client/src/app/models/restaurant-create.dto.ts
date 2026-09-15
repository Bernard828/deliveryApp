import { RestaurantHourCreateDto } from "./operating-hours.model";
import {  AddressDto } from "./address.model";

export interface RestaurantCreateDto {
  name: string;
  description: string;
  isActive: boolean;

  cuisineTypeId: number | null;

  imageUrl?: string;
  searchTags?: string[];
  operatingHours?: RestaurantHourCreateDto[];

  //addressId: number;
  address?: AddressDto;
}
