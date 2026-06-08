export interface RestaurantDto {
  restaurantId: number;
  name: string;
  cuisineTypeId: number;
  cuisine: CuisineType;
  price: number;
  imageUrl: string;
  address: string;
  isCurrentlyOpen: boolean;
  operatingHours: RestaurantHourDto[];
}

export interface RestaurantHourDto {
  dayOfWeek: number;
  openTime: string;
  closeTime: string;
}

export interface CuisineType {
  cusineTypeId: number;
  name: string;
}
