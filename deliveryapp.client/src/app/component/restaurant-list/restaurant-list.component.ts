import { Component, OnInit, inject, signal } from '@angular/core';

import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RestaurantService } from "../../services/restaurant.service";
import { RestaurantSearchDto,RestaurantCreateDto} from '../../models/restuarant.model';

//PrimeNG Imports
//import { } from 'primeng/card';

@Component({
  selector: 'app-restaurant-list',
  standalone: false,
  templateUrl: './restaurant-list.component.html',
  styleUrl: './restaurant-list.component.css'
})
export class RestaurantListComponent {

}
