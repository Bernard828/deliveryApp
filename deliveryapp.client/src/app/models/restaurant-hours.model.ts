export interface RestaurantHours {
}
export interface RestaurantHourDto {
  //NEW Begins
  modayOpen: string;
  mondayClose: string;
  tuesdayOpen: string;
  tuesdayClosed: string;
  wednesdayOpen: string;
  wednesdayClosed: string;
  thursdayOpen: string;
  thursdayClosed: string;
  fridayOpen: string;
  fridayClosed: string;
  saturdayOpen: string;
  saturdayClosed: string;
  sundayOpen: string;
  sundayClosed: string;
  //OLD Begins
  dayOfWeek: number;
  openTime: string;
  closeTime: string;
  //OLD Ends
}
