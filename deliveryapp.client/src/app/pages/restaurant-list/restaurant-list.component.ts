import {
  Component,
  ChangeDetectionStrategy,
  inject,
  signal,
  OnInit
} from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';

import { RestaurantService } from "../../services/restaurant.service";
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
import { TableModule } from 'primeng/table';
//import { InputTextareaModule } from 'primeng/inputtextarea';
import { ChipModule } from 'primeng/chip';
import { InputNumberModule } from 'primeng/inputnumber';
import { TagModule } from 'primeng/tag';
import { ProgressSpinnerModule } from 'primeng/progressspinner';

@Component({
  selector: 'app-restaurant-list',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    CardModule,
    ButtonModule,
    DialogModule,
    InputTextModule,
    //InputTextareaModule,
    ChipModule,
    InputNumberModule,
    TagModule,
    ProgressSpinnerModule
  ],
  templateUrl: './restaurant-list.component.html',
  styleUrls: ['./restaurant-list.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class RestaurantListComponent implements OnInit {
  private fb = inject(FormBuilder);
  private restaurantService = inject(RestaurantService);

  restaurants = signal<RestaurantDto[]>([]);

  //Dialogs
  displayCreateDialog = signal<boolean>(false);
  displayMenuDialog = signal<boolean>(false);

  //selected restaurant for menu modal
  selectedRestaurantMenu = signal<RestaurantSearchDto | null>(null);

  //constructor() { this.loadRestaurants(); }

  createForm!: FormGroup;

  ngOnInit(): void {
    this.loadRestaurants();
    this.initForm();
  }

  loadRestaurants(): void {
    this.restaurantService.getAll().subscribe(restaurants =>
      this.restaurants.set(restaurants));
  }

  initForm(): void {
    this.createForm = this.fb.group({
      name: ['', [Validators.required]],
      description: ['', [Validators.required]],
      cuisineTypeId: [null, [Validators.required]],
      searchTags: [[]]
    });
  }

  openCreateModal(): void {
    this.createForm.reset({ searchTags: [] });
    this.displayCreateDialog.set(true);
  }

  selectRestaurant(id: number): void {
    this.restaurantService.getbyId(id).subscribe({
      next: (data) => {
        this.selectedRestaurantMenu.set(data);
        this.displayMenuDialog.set(true);
      }
    });
  }

  onSubmit(): void {
    if (this.createForm.invalid) return;

    const dto = this.createForm.value as RestaurantCreateDto;

    this.restaurantService.create(dto).subscribe(() => {
      this.displayCreateDialog.set(false);
      this.loadRestaurants();
    });
  }
}
