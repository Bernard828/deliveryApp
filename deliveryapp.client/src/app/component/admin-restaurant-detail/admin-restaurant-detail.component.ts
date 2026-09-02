import { Component, OnInit, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { RestaurantService } from '../../../services/restaurant.service';
import { RestaurantDto } from '../../models/restuarant.model';
import { MenuItemDto } from '../../models/menuItem.model';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MenuItemService } from '../../../services/menu-item.service';
import { OperatingHourDto } from '../../models/restaurant-hours.model';

import { ButtonModule } from 'primeng/button';
//import { DropdownModule } from 'primeng/dropdown';
//import { InputSwitchModule } from 'primeng/inputswitch';
//import { TableModule } from 'primeng/table';
import { PaginatorModule } from 'primeng/paginator';


@Component({
  selector: 'app-admin-restaurant-detail',
  standalone: true,
  templateUrl: './admin-restaurant-detail.component.html',
  styleUrls: ['./admin-restaurant-detail.component.css'],
  imports: [
   // TableModule,
    ButtonModule,
    //DropdownModule,
   // InputSwitchModule,
    PaginatorModule,
    ReactiveFormsModule
  ]
})
export class AdminRestaurantDetailComponent {
  private route = inject(ActivatedRoute);
  private restaurantService = inject(RestaurantService);
  private menuItemService = inject(MenuItemService);

  restaurantId!: number;
  restaurant!: RestaurantDto;

  menuItems: MenuItemDto[] = [];
  totalMenuItems = 0;
  menuPage = 0;
  menuPageSize = 10;
  menuIsActiveFilter: boolean | null = (null);

  hoursForm = new FormGroup({
    modayOpen: new FormControl(''),
    mondayClose: new FormControl(''),
    tuesdayOpen: new FormControl(''),
    tuesdayClosed: new FormControl(''),
    wednesdayOpen: new FormControl(''),
    wednesdayClosed: new FormControl(''),
    thursdayOpen: new FormControl(''),
    thursdayClosed: new FormControl(''),
    fridayOpen: new FormControl(''),
    fridayClosed: new FormControl(''),
    saturdayOpen: new FormControl(''),
    saturdayClosed: new FormControl(''),
    sundayOpen: new FormControl(''),
    sundayClosed: new FormControl(''),
  });

  ngOnInit() {
    this.restaurantId = Number(this.route.snapshot.paramMap.get('id'));
    this.loadRestaurant();
    this.loadMenuItems();
  }

  loadRestaurant() {
    this.restaurantService.getById(this.restaurantId)
      .subscribe(r => {
        this.restaurant = r;
        if (r.operatingHours) {
          //this.hoursForm.patchValue(r.operatingHours);
        }
      });
  }

  loadMenuItems() {
    this.menuItemService.getByRestaurantId(this.restaurantId, {
      page: this.menuPage,
      pageSize: this.menuPageSize,
      isActive: this.menuIsActiveFilter ?? undefined
    })
      .subscribe(result => {
        this.menuItems = result.items;
        this.totalMenuItems = result.totalCount;
      });
  }

  onMenuPageChange(event: any) {
    this.menuPage = event.page;
    this.menuPageSize = event.rows;
    this.loadMenuItems();
  }

  onMenuIsActiveFilterChange(value: boolean | null) {
    this.menuIsActiveFilter = value;
    this.menuPage = 0;
    this.loadMenuItems();
  }

  saveHours() {
    const updated: RestaurantDto = {
      ...this.restaurant,
      //operatingHours: this.hoursForm.value as OperatingHourDto
    };
    this.restaurantService.update(this.restaurantId, updated)
      .subscribe(() => {
        //maybe show toast
      });
  }

}
