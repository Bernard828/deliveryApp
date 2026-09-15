import { CommonModule } from '@angular/common';
import {
  Component,
  ChangeDetectionStrategy,
  inject,
  signal,
  OnInit
} from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
  FormsModule
} from '@angular/forms';

//Serivces
import { RestaurantService } from "../../../services/restaurant.service";
import {
  RestaurantDto,
  RestaurantCreateDto,
} from '../../models/restuarant.model';

//PrimeNG Imports
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { ChipModule } from 'primeng/chip';
import { InputNumberModule } from 'primeng/inputnumber';
import { TagModule } from 'primeng/tag';
import { ProgressSpinnerModule } from 'primeng/progressspinner';

import { RestaurantMenuModalComponent } from '../../component/restaurant-menu-modal/restaurant-menu-modal.component';
@Component({
  selector: 'app-restaurant-list',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    CardModule,
    ButtonModule,
    DialogModule,
    InputTextModule,
    ChipModule,
    InputNumberModule,
    TagModule,
    ProgressSpinnerModule,
    RestaurantMenuModalComponent
  ],
  templateUrl: './restaurant-list.component.html',
  styleUrls: ['./restaurant-list.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class RestaurantListComponent implements OnInit {
  public readonly restaurantService = inject(RestaurantService);
  private readonly fb = inject(FormBuilder);

  restaurants = signal<RestaurantDto[]>([]);

  // Selected restaurant for menu modal

  // Pager
  currentPage = signal(1);
  pageSize = signal(10);
  totalCount = signal(0);
  totalPages = signal(0);

  // Dialogs
  displayCreateDialog = signal(false);
  displayMenuDialog = signal(false);

  selectedRestaurantMenu = signal<RestaurantDto | null>(null);
  selectedRestaurant = signal<RestaurantDto | null>(null);
  submitting = signal(false);

  createForm!: FormGroup;

  ngOnInit(): void {
    this.initForm();
    this.loadRestaurants();
  }

  loadRestaurants(): void {

    this.restaurantService.getPaged(
      this.currentPage(),
      this.pageSize(),
      true
    ).subscribe({
      next: (result) => {
        this.restaurants.set(result.items);
        this.totalCount.set(result.totalCount);
        this.totalPages.set(result.totalPages);
      },
      error: err => {
        console.error('Error loading restaurants:', err);
      }
    });
  }

  goToPage(page: number): void {

    if (page < 1 || page > this.totalPages()) {
      return;
    }
    this.currentPage.set(page);
    this.loadRestaurants();
  }

  nextPage(): void {
    this.goToPage(this.currentPage() + 1)
  }

  previousPage(): void {
    this.goToPage(this.currentPage() - 1)
  };

  initForm(): void {
    this.createForm = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(150)]],
      description: ['', [Validators.required, Validators.maxLength(1000)]],
      isActive: [true],
      cuisineTypeId: [null],
      imageUrl: ['', Validators.maxLength(500)],
      address: this.fb.group({
        line1: ['', [Validators.required, Validators.maxLength(150)]],
        line2: ['', [Validators.maxLength(150)]],
        city: ['', [Validators.required, Validators.maxLength(100)]],
        state: ['', [Validators.required, Validators.maxLength(50)]],
        postalCode: ['', [Validators.required, Validators.maxLength(20)]],
        country: ['USA', [Validators.required, Validators.maxLength(100)]],
      })
    });


  }

  openCreateModal(): void {
    this.createForm?.reset({
      name: '',
      description: '',
      isActive: true,
      cuisineTypeId: null,
      imageUrl: '',
      address: {
        line1: '',
        line2: '',
        city: '',
        state: '',
        postalCode: '',
        country: 'USA',
      }
    });

    this.displayCreateDialog.set(true);
  }

  closeCreateModal(): void {
    this.displayCreateDialog.set(false);
  }

  onSubmit(): void {

    if (!this.createForm || this.createForm.invalid) {
      this.createForm?.markAllAsTouched();
      return;
    }

    const dto = this.createForm.value as RestaurantCreateDto;

    this.submitting.set(true);

    // Call the service to create the restaurant
    this.restaurantService.create(dto).subscribe({
      next: (created) => {
        this.submitting.set(false);
        this.displayCreateDialog.set(false);

        //reload current page.
        this.loadRestaurants();
      },
      error: (err) => {
        this.submitting.set(false);
        console.error('Failed to create restaurant', err);
      },
      complete: () => {
        this.submitting.set(false);
      }
    });
    this.loadRestaurants();
  }

  selectRestaurant(restaurantId: number): void {

    const restaurant = this.restaurants().find(r =>
      r.restaurantId === restaurantId);

    if (!restaurant) {
      return;
    }

    this.selectedRestaurant.set(restaurant);
  }

  trackById(index: number,
    restaurant: RestaurantDto): number {
    return restaurant.restaurantId;
  }

  // resetForm(close = true): void {
  //   if (this.createForm) {
  //     this.createForm.reset();
  //   } if (close) {
  //     this.displayCreateDialog.set(false);
  //   }
  // }

  // onCancel(): void {
  //   if (this.createForm)
  //     this.createForm.reset();
  //   this.displayCreateDialog.set(false);
  // }

  onCloseClicked(): void {
    //if (this.createForm) this.createForm.reset();
    this.displayCreateDialog.set(false);
  }
}
