import { Component, OnInit, inject, signal, ChangeDetectionStrategy } from '@angular/core';
import { CommonModule } from '@angular/common'; }
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RestaurantService } from "../../services/restaurant.service";
import { RestaurantSearchDto, RestaurantCreateDto } from '../../models/restuarant.model';

//PrimeNG Imports
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { InputTextareaModule } from 'primeng/inputtextarea';
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
    InputTextareaModule,
    ChipModule,
    InputNumberModule,
    TagModule,
    ProgressSpinnerModule
  ],
  templateUrl: './restaurant-list.component.html',
  changeDetection: ChangeDetectionStrategy.Eager,
  styleUrl: './restaurant-list.component.css'
})
export class RestaurantListComponent implements OnInit {
  restaurantService = inject(RestaurantService);
  private fb = inject(FormBuilder);

  //modal display toggles
  displayCreateDialog = signal<boolean>(false);
  displayMenuDialog = signal<boolean>(false);

  //selected restaurant focus
  selectedRestaurantMenu = signal<RestaurantSearchDto | null>(null);

  createForm!: FormGroup;

  ngOnInit(): void {
    this.restaurantService.getAll().subscribe();
    this.initForm();
  }

  initForm(): void {
    this.createForm = this.fb.group({
      name: ['', [Validators.required]],
      description: ['', [Validators.required]],
      cuisineTypeId: [null, [Validators.required]]],
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

}
