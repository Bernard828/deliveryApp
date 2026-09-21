import { AddressDto } from "./address.model";
import { MenuItemDto } from "./menu-item.model";
import { OperatingHoursDto, OperatingHoursCreateDto, OperatingHoursUpdateDto } from "./operating-hours.model";
export interface RestaurantDto {
  restaurantId: number|null;
  name: string;
  description: string|null;
  isActive: boolean|null;
  imageUrl: string|null;

  cuisineTypeId: number | null;
  cuisineTypeName?: string | null;

  address?: AddressDto | null;

  isCurrentlyOpen: boolean;

  operatingHours: OperatingHoursDto[];

 // searchTags: string[];
  restaurantTagIds:number[];
  menuItems?: MenuItemDto[];
}

export interface RestaurantCreateDto {

  restaurantId:number|null;
  name: string;
  description: string|null;
  isActive: boolean|null;

  cuisineTypeId: number | null;

  imageUrl: string|null;
  //searchTags: string[];
    restaurantTagIds:number[];

 // operatingHours: OperatingHoursCreateDto[];

  //addressId: number;
  address: AddressDto|null;
}


export interface RestaurantUpdateDto {
  restaurantId: number|null;
  name: string;
  description: string|null;
  isActive: boolean|null;

  cuisineTypeId: number | null;

  imageUrl: string|null;
  //searchTags: string[];
    restaurantTagIds:number[];

  //operatingHours: OperatingHoursUpdateDto[];

  //addressId: number;
  address: AddressDto|null;
}

