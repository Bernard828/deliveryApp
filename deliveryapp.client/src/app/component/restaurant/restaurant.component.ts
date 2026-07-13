import { Component, ChangeDetectionStrategy, EventEmitter, Input, Output, OnChanges, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
//import { DropdownModule } from 'primeng/dropdown';
import { ButtonModule } from 'primeng/button';
import { RestaurantDto, RestaurantSearchDto } from '../../models/restuarant.model';

@Component({
  selector: 'app-restaurant',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, InputTextModule, ButtonModule],
  templateUrl: './restaurant.component.html',
  changeDetection: ChangeDetectionStrategy.Eager,
  styleUrl: './restaurant.component.css'
})
export class RestaurantComponent implements OnChanges {
  @Input() restaurant: RestaurantDto | null = null;
  @Input() mode: 'create' | 'edit' = 'create';

  @Output() save = new EventEmitter<RestaurantDto>();
  @Output() cancel = new EventEmitter<void>();

  statusOptions = [
    { label: 'Active', value: 'Active' },
    { label: 'Inactive', value: 'Inactive' }
  ];

  form = this.fb.group({
    id: [0],
    name: ['', Validators.required],
    status: ['Active', Validators.required]
  });

  constructor(private readonly fb: FormBuilder) { }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['restaurant']) {
      if (this.restaurant) {
        this.form.patchValue(this.restaurant);
      } else {
        this.form.reset({ id: 0, name: '', status: 'Active' });
      }
    }
  }

  onSubmit(): void {
    if (this.form.valid) {
      this.save.emit(this.form.value as RestaurantDto);
    }
  }
}

