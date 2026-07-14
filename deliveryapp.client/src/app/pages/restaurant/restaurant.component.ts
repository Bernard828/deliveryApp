import {
  Component,
  ChangeDetectionStrategy,
  EventEmitter,
  Input,
  Output,
  OnChanges,
  SimpleChanges
} from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  ReactiveFormsModule,
  FormBuilder,
  Validators,
  FormGroup
} from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { RestaurantDto } from '../../models/restuarant.model';

@Component({
  selector: 'app-restaurant',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, InputTextModule, ButtonModule],
  templateUrl: './restaurant.component.html',
  styleUrls: ['./restaurant.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RestaurantComponent implements OnChanges {
  @Input() restaurant: RestaurantDto | null = null;
  @Input() mode: 'create' | 'edit' = 'create';

  @Output() save = new EventEmitter<RestaurantDto>();
  @Output() cancel = new EventEmitter<void>();

  form!: FormGroup;

  constructor(private readonly fb: FormBuilder) {
    this.form = this.fb.group({
      id: [0],
      name: ['', Validators.required],
      status: ['Active', Validators.required]
    });
  }


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

