export interface OperatingHoursDto {
  //NEW Begins
  // modayOpen: string;
  // mondayClose: string;
  // tuesdayOpen: string;
  // tuesdayClosed: string;
  // wednesdayOpen: string;
  // wednesdayClosed: string;
  // thursdayOpen: string;
  // thursdayClosed: string;
  // fridayOpen: string;
  // fridayClosed: string;
  // saturdayOpen: string;
  // saturdayClosed: string;
  // sundayOpen: string;
  // sundayClosed: string;

  //Different model Begins
  operatingHourId: number;
  restaurantId: number;
  dayOfWeek: number;
  dayName: string;
  isClosed: boolean;
  openTime: string | null;
  closeTime: string | null;
}

export interface OperatingHoursCreateDto {
  restaurantId: number;
  dayOfWeek: number;
  isClosed: boolean;
  openTime: string | null;
  closeTime: string | null;
}

export interface OperatingHoursUpdateDto {
  operatingHourId: number;
  restaurantId: number;
  dayOfWeek: number;
  dayName: string;
  isClosed: boolean;
  openTime: string | null;
  closeTime: string | null;
}
