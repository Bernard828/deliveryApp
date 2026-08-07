import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { RestaurantService } from '../../../services/restaurant.service';
import { RestaurantDto } from '../../models/restuarant.model';


@Component({
  selector: 'app-admin-restaurant-list',
  standalone: true,
  templateUrl: './admin-restaurant-list.component.html',
  styleUrls: ['./admin-restaurant-list.component.css'],
  imports: [
    CommonModule, FormsModule
  ]
})
export class AdminRestaurantListComponent implements OnInit {
  private restaurantService = inject(RestaurantService);

  restaurants: RestaurantDto[] = [];
  loading: boolean = false;

  displayEditModal: boolean = false;
  selectedRestaurant: RestaurantDto | null = null;

  totalRecords = 0;

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

  openEditModal(restaurant: RestaurantDto): void {
    this.selectedRestaurant = { ...restaurant };
    this.displayEditModal = true;
  }

  closeModal(): void {
    this.displayEditModal = false;
    this.selectedRestaurant = null;
  }

  saveRestaurant(): void {
    if (!this.selectedRestaurant) return;

    this.loading = true;
    this.restaurantService.updateNew(this.selectedRestaurant.restaurantId, this.selectedRestaurant)
      .subscribe({
        next: () => {
          this.closeModal();
          this.loadRestaurants();
        },
        error: err => {
          console.error(err);
          this.loading = false;
        }
      })
  }

  onRowSelect(restaurant: RestaurantDto) {}
}
