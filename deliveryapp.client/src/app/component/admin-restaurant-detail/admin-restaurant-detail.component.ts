import { Component, OnInit, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { RestaurantService } from '../../../services/restaurant.service';
import { RestaurantDto, RestaurantUpdateDto } from '../../models/restuarant.model';
import { MenuItemDto } from '../../models/menu-item.model';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MenuItemService } from '../../../services/menu-item.service';

import { ButtonModule } from 'primeng/button';
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
    this.menuItemService.getByRestaurantId(this.restaurantId)
      .subscribe({
        next: result => {
          this.menuItems = result;
          //this.totalMenuItems = result.totalCount;
        }
      })
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

  saveHours(dto:RestaurantUpdateDto) {
    this.restaurantService.update(this.restaurantId, dto)
      .subscribe(() => {
        //maybe show toast
      });
  }

}
