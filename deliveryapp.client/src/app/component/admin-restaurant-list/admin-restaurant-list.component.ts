import { Component, OnInit, inject } from '@angular/core';
import { RestaurantService } from '../../../services/restaurant.service';
import { RestaurantDto } from '../../models/restuarant.model';


@Component({
  selector: 'app-admin-restaurant-list',
  standalone: true,
  templateUrl: './admin-restaurant-list.component.html',
  styleUrls: ['./admin-restaurant-list.component.css'],
  imports: [
   // TableModule,
   // DropdownModule,
    //InputSwitchModule,
    //PaginatorModule
  ]
})
export class AdminRestaurantListComponent implements OnInit {
  private restaurantService = inject(RestaurantService);
  restaurants: RestaurantDto[] = [];
  totalRecords = 0;
  loading = false;

  page = 0;
  pageSize = 10;
  isActiveFilter: boolean | null = null;

  ngOnInit() {
    this.loadRestaurants();
  }

  loadRestaurants() {
    this.loading = true;
    this.restaurantService
      .getAllNew({
        page: this.page,
        pageSize: this.pageSize,
        isActive: this.isActiveFilter ?? undefined
      })
      .subscribe(result => {
        this.restaurants = result.items;
        this.totalRecords = result.totalCount;
        this.loading = false;
      });
  }

  onPageChange(event: any) {
    this.page = event.page;
    this.pageSize = event.rows;
    this.loadRestaurants();
  }

  onIsActiveFilterChange(value: boolean | null) {
    this.isActiveFilter = value;
    this.page = 0;
    this.loadRestaurants();
  }

  onRowSelect(restaurant: RestaurantDto) {
    //navigate to detail page
    //e.g. this.router.navigate(['/admin/restaurants',restaurant.restaurantId]);
  }
}
