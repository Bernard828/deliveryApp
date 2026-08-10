import {
  Component,
  OnInit,
  inject,
  signal,
  ChangeDetectionStrategy,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';

import { RestaurantService } from '../../../services/restaurant.service';
import { Restaurant, RestaurantDto } from '../../models/restuarant.model';


@Component({
  selector: 'app-admin-restaurant-list',
  standalone: true,
  templateUrl: './admin-restaurant-list.component.html',
  styleUrls: ['./admin-restaurant-list.component.css'],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,

  ]
})
export class AdminRestaurantListComponent implements OnInit {
  restaurantList = signal<RestaurantDto[]>([]);
  restaurants: RestaurantDto[] = [];

  loading: boolean = false;

  isModalOpen = false;
  isEditMode = false;
  displayEditModal: boolean = false;
  selectedRestaurant: RestaurantDto | null = null;
  selectedRestaurantId: number | null = null;

  restaurantForm!: FormGroup;

  totalRecords = 0;
  page = 1;
  pageSize = 10;
  isActiveFilter: boolean | null = null;

  //Regex Patterns
  // Matches "09:00 AM - 10:00 PM" or "24 Hours" or standard "09:00-22:00" strings cleanly
  hoursRegex = /^(?:[0-1]?[0-9]|2[0-3]):[0-5][0-9]\s?(?:AM|PM)?\s?-\s?(?:[0-1]?[0-9]|2[0-3]):[0-5][0-9]\s?(?:AM|PM)?$/i;
  // Matches standard HTTP/HTTPS image URL links safely
  urlRegex = /^(https?:\/\/.*\.(?:png|jpg|jpeg|gif|webp|svg))$/i;

  private restaurantService = inject(RestaurantService);
  private fb = inject(FormBuilder);

  constructor() {
    this.initForm();
  }

  ngOnInit() {
    this.loadRestaurants();
    this.initForm();
  }


  initForm(): void {
    this.restaurantForm = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(200)]],
      description: ['', [Validators.required, Validators.maxLength(200)]],
      address: ['', [Validators.required]],
      cuisineTypeId: [null, [Validators.required]],
      imageUrl: ['', [Validators.pattern(this.urlRegex)]],
      displayHours: ['', [Validators.required, Validators.pattern(this.hoursRegex)]],
      isActive: [true]
    });
  }

  loadRestaurantsOLD() {
    this.loading = true;
    this.restaurantService
      .getAllNew({
        page: this.page,
        pageSize: this.pageSize,
        isActive: this.isActiveFilter ?? undefined
      })
      .subscribe({
        next: (result) => {
          this.restaurants = result.items;
          this.totalRecords = result.totalCount;
          this.loading = false;
        }, error: (err) => {
          console.error(err);
          this.loading = false;
        }
      });
  }

  loadRestaurants(): void {
    this.restaurantService.getAll().subscribe(list => {
      this.restaurantList.set(list || []);
    });
  }

  onPageChange(event: any) {
    this.page = event.page;
    this.pageSize = event.rows;
    this.loadRestaurants();
  }

  openCreateModal(): void {
    this.isEditMode = false;
    this.selectedRestaurantId = null;
    this.restaurantForm.reset({ cusineTypeId: null, isActive: true });
    this.isModalOpen = true;
  }

  openEditModal(restaurant: RestaurantDto): void {
    this.isEditMode = true;
    this.selectedRestaurantId = restaurant.restaurantId;
    const standardHoursString = restaurant.operatingHours && restaurant.operatingHours.length > 0
      ? `${restaurant.operatingHours[0].openTime.substring(0, 5)} - ${restaurant.operatingHours[0].closeTime.substring(0, 5)}`
      : '09:00 - 22:00';
    // this.selectedRestaurant = { ...restaurant };
    // this.displayEditModal = true;

    this.restaurantForm.setValue({
      name: restaurant.name,
      description: restaurant.description,
      address: restaurant.address,
      cuisineTypeId: restaurant.cuisineTypeId,
      imageUrl: restaurant.imageUrl,
      displayHours: standardHoursString
    });
    this.displayEditModal = true;
    this.isModalOpen = true;
  }

  closeModal(): void {
    this.isModalOpen = false;
    this.displayEditModal = false;
    this.selectedRestaurantId = null;
    this.restaurantForm.reset();
    // this.selectedRestaurant = null;
  }

  saveRestaurant(): void {
    if (this.restaurantForm.invalid) {
      this.restaurantForm.markAllAsTouched();
      return;
    }

    this.loading = true;
    const formValues = this.restaurantForm.value;

    if (this.isEditMode && this.selectedRestaurantId !== null) {
      // Build full updated payload structure
      const updateDto: RestaurantDto = {
        restaurantId: this.selectedRestaurantId,
        //isActive: true,
        //isCurrentlyOpen: false,
        ...formValues
      };

      this.restaurantService.updateNew(this.selectedRestaurantId, updateDto).subscribe({
        next: () => {
          this.closeModal();
          this.loadRestaurants();
        },
        error: (err) => {
          console.error(err);
          this.loading = false;
        }
      });
    }

    //   this.restaurantService.update(this.selectedRestaurantId, updateDto).subscribe({
    //     next: () => {
    //       this.loadRestaurants();
    //       this.closeModal();
    //     }
    //   });
    // } else {
    //   const createDto = {
    //     ...formValues,
    //     searchTags: []
    //   };

    //   this.restaurantService.create(createDto).subscribe({
    //     next: () => { this.loadRestaurants(); this.closeModal(); }
    //   });
    // }

    // if (!this.selectedRestaurant) return;
    // this.loading = true;
    // this.restaurantService.updateNew(this.selectedRestaurant.restaurantId, this.selectedRestaurant)
    //   .subscribe({
    //     next: () => {
    //       this.closeModal();
    //       this.loadRestaurants();
    //     },
    //     error: err => {
    //       console.error(err);
    //       this.loading = false;
    //     }
    //   });
    else {
      const createDto = {
        ...formValues,
        searchTags: []
      };

      this.restaurantService.create(createDto).subscribe({
        next: () => {
          this.closeModal();
          this.loadRestaurants();
        },
        error: (err) => {
          console.error(err);
          this.loading = false;
        }
      });
    }
  }

  toggleActiveState(restaurant: RestaurantDto): void {
    const updateDto: RestaurantDto = {
      ...restaurant,
      isActive: !restaurant.isActive
    };

    // this.restaurantService.update(restaurant.restaurantId, updateDto).subscribe({
    //   next: () => this.loadRestaurants()
    // });

    this.restaurantService.updateNew(restaurant.restaurantId, updateDto).subscribe({
      next: () => this.loadRestaurants(),
      error: (err) => console.error(err)
    });
  }

  onIsActiveFilterChange(value: boolean | null) {
    this.isActiveFilter = value;
    this.page = 1;
    this.loadRestaurants();
  }

  onMenuManagement(restaurantId: number) {
    console.log('Routing execution focus context directed towards Menu Items: ', restaurantId);
  }

  navigateToMenu(restaurantId: number): void {
    console.log('Targeting menu context for item ID:', restaurantId);
  }

  onRowSelect(restaurant: RestaurantDto) { }
}
