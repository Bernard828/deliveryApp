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
  RestaurantSearchDto,
  RestaurantCreateDto,
  RestaurantDto
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
  selectedRestaurantMenu = signal<RestaurantSearchDto | null>(null);

  // Dialogs
  displayCreateDialog = signal(false);
  displayMenuDialog = signal(false);

  createForm!: FormGroup;
  submitting = signal(false);

  ngOnInit(): void {
    this.initForm(false);
    this.loadRestaurants();
  }

  loadRestaurants(): void {
    this.restaurantService.getAll().subscribe(list => {
      this.restaurants.set(list || []);
    });
  }

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
    this.initForm(true);
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
    if (this.createForm)
      this.createForm.reset();
    this.displayCreateDialog.set(false);
  }

  onSubmit(): void {
    if (!this.createForm || this.createForm.invalid) {
      this.createForm.markAllAsTouched(); return;
    }
    const dto = this.createForm.value as RestaurantCreateDto;
        this.submitting.set(true);
            this.restaurantService.create(dto).subscribe({
      next: (created: RestaurantDto) => {
        this.restaurants.update(list => [created, ...list]);

        this.resetForm(true);
      },
      error: (err) => {
        console.error('Failed to create restaurant', err);
      },
      complete: () => {
        this.submitting.set(false);
      }
    });
  }

  selectRestaurant(id: number): void {
    this.restaurantService.getById(id).subscribe({
      next: (data) => {
        this.selectedRestaurantMenu.set(data);
        this.displayMenuDialog.set(true);
      },
      error: (err) => console.error(err)
    });
  }

  trackById(index: number, item: RestaurantDto) {
    return item.restaurantId;
  }
}
