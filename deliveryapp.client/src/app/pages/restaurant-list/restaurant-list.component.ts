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
  private fb = inject(FormBuilder);
  public restaurantService = inject(RestaurantService);

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

  createForm!: FormGroup;
  submitting = signal(false);

  ngOnInit(): void {
    this.initForm(false);
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

  initForm(openAfterInit = true): void {
    this.createForm = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(200)]],
      description: ['', [Validators.required, Validators.maxLength(1000)]],
    });
    if (openAfterInit) {
      this.displayCreateDialog.set(true);
    }
  }

  openCreateModal(): void {
    this.createForm.reset({
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
        zipCode: '',
        country:'USA',
      }
    });

    this.displayCreateDialog.set(true);

    //this.initForm(true);
  }

  resetForm(close = true): void {
    if (this.createForm) {
      this.createForm.reset();
    } if (close) {
      this.displayCreateDialog.set(false);
    }
  }

  onCancel(): void {
    if (this.createForm)
      this.createForm.reset();
    this.displayCreateDialog.set(false);
  }

  onCloseClicked(): void {
    //if (this.createForm) this.createForm.reset();
    this.displayCreateDialog.set(false);
  }
  closeCreateModal(): void {
    this.displayCreateDialog.set(false);
  }

  onSubmit(): void {
    if (!this.createForm || this.createForm.invalid) {
      this.createForm.markAllAsTouched();
      return;
    }

    const dto = this.createForm.value as RestaurantCreateDto;

    this.submitting.set(true);

    // Call the service to create the restaurant
    this.submitting.set(false);
    this.displayCreateDialog.set(false);
    this.restaurantService.create(dto).subscribe({
      next: (created/*: RestaurantDto */) => {
        this.submitting.set(false);
        this.displayCreateDialog.set(false);

        //reload current page.
        this.loadRestaurants();

       // this.restaurants.update(list => [created, ...list]);

        //this.resetForm(true);
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

    this.selectRestaurant.set(restaurant);

    // this.selectedRestaurantMenu.set(restaurant);
    // this.displayMenuDialog.set(true);

    // this.restaurantService.getById(restaurantId).subscribe({
    //   next: (data) => {
    //     this.selectedRestaurantMenu.set(data);
    //     this.displayMenuDialog.set(true);
    //   },
    //   error: (err) => console.error(err)
    // });
  }

  trackById(index: number,
    restaurant: RestaurantDto):number {
    return restaurant.restaurantId;
  }
}
