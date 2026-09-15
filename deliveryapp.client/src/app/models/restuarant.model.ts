import { AddressDto } from "./address.model";
import { MenuItemDto } from "./menu-item.model";
import { RestaurantHoursDto, RestaurantHourCreateDto } from "./operating-hours.model";
export interface RestaurantDto {
  restaurantId: number;
  name: string;
  description: string;
  isActive: boolean;
  imageUrl: string;

  cuisineTypeId: number | null;
  cuisineTypeName?: string | null;

  address?: AddressDto | null;

  isCurrentlyOpen: boolean;

  operatingHours: RestaurantHoursDto[];

  searchTags: string[];

  menuItems?: MenuItemDto[];
}

export interface RestaurantCreateDto {
  name: string;
  description: string;
  isActive: boolean;

  cuisineTypeId: number | null;

  imageUrl?: string;
  searchTags: string[];
  operatingHours: RestaurantHourCreateDto[];

  //addressId: number;
  address?: AddressDto;
}


export interface RestaurantUpdateDto {
  restaurantId: number;
  name: string;
  description: string;
  isActive: boolean;

  cuisineTypeId: number | null;

  imageUrl?: string;
  searchTags: string[];
  operatingHours: RestaurantHourCreateDto[];

  //addressId: number;
  address?: AddressDto;
}

