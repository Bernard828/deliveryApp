import { Component ,OnInit,inject,signal} from '@angular/core';
import { CommonModule } from '@angular/common';
import{FormArray,FormBuilder,FormControl,FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';

import { RestaurantService } from '../../../services/restaurant.service';
import { CuisineTypeService } from '../../../services/cuisine-type.service';
import { RestaurantTagService } from '../../../services/restaurant-tag.service';
import{OperatingHourService} from '../../../services/operating-hour.service';


import { CuisineTypeDto } from '../../models/cuisine-type.model';
import { RestaurantTagDto } from '../../models/restaurant-tag-dto.model';
import { RestaurantCreateDto, RestaurantUpdateDto } from '../../models/restuarant.model';
import{FontAwesomeModule} from'@fortawesome/angular-fontawesome';
import { faCheck } from '@fortawesome/free-solid-svg-icons';
@Component({
  selector: 'app-restaurant-edit',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FontAwesomeModule],
  templateUrl: './restaurant-edit.component.html',
  styleUrls: ['./restaurant-edit.component.css']
})
export class RestaurantEditComponent {

  //Icons
  faCheck=faCheck;

  private readonly fb= inject(FormBuilder);

  private readonly restaurantService=inject(RestaurantService);

  private readonly cuisineService= inject(CuisineTypeService);

  private readonly restaurantTagService = inject(RestaurantTagService);

private readonly hoursService =inject(OperatingHourService);

readonly cuisines= signal<CuisineTypeDto[]>([]);

readonly restaurantTags= signal<RestaurantTagDto[]>([]);

readonly loading = signal(false);

readonly saving=signal(false);

readonly restaurantId= signal<number|null>(null);


// Main Form
readonly form = this.fb.group({

  name:['',[Validators.required, Validators.maxLength(150)]],

  description:['',[Validators.maxLength(500)]],

  cusisneTypeId:[null as number |null],

  imageUrl:['', Validators.maxLength(500)],

  isActive:[true],

  address:this.fb.group({

    line1:['',Validators.required],

    line2:[''],

    city:['',Validators.required],

    state:['',Validators.required],

    zipCode:['',Validators.required],

    country:['USA',Validators.required]
  }),

  restaurantTagIds: this.fb.control<number[]>([]),

  operatingHours:this.fb.array([]),

  restaurantId:[0]
});

// FormArray access
get operatingHours():FormArray{

  return this.form.controls.operatingHours;
}

ngOnInit  ():void{

  this.loadLookups();

  this.createDefaultOperatingHours();
}

//Lookup Data
private loadLookups():void{

  this.cuisineService.getAll().subscribe({
    next:cuisines=> this.cuisines.set(cuisines)
  });

  this.restaurantTagService.getAll().subscribe({
        next:tags=>this.restaurantTags.set(tags)
  });
}

// Create Seven Editable Hour Rows
private createDefaultOperatingHours():void{

  const days=[
        'Sunday','Monday','Tuesday','Wednesday','Thursday','Friday','Saturday'
  ];

  days.forEach((dayName,dayOfWeek)=>{

    this.operatingHours.push(

      this.fb.group({

        dayOfWeek:[dayOfWeek], dayName:[dayName], isClosed:[false], openTime:['09:00'], closeTime:['21:00']
      })
    );
  });
}

//Retaurant Tag Selection

isTagSelected(tagId:number):boolean{

  return this.selectedTagIds.includes(tagId);

}
  get selectedTagIds():number[]{

    return this.form.controls.restaurantTagIds.value??[];
  }

  toggleTag(tagId:number):void{

    const current = [
      ...this.selectedTagIds
    ];

    const index= current.indexOf(tagId);

    if(index>=0){current.splice(index,1);}

    else{current.push(tagId);}

    this.form.controls.restaurantTagIds.setValue(current);
  }

  //Operatinh Hour helpers

  getHourGroup(index:number):FormGroup{return this.operatingHours.at(index)as FormGroup;}

  toggleClosed(index:number):void{

  const group=this.getHourGroup(index);

  const closed= group.get('isClosed')?.value;

  if(closed){group.patchValue({

    openTime:null,

    closeTime:null
  });}else{group.patchValue({

    openTime:'09:00',

    closeTime:'21:00'
  });
  }
  }

  copyMondaytoWeekdays():void{

    const monday= this.getHourGroup(1).value;

    for(let i=2;i<=5;i++){

      this.getHourGroup(i).patchValue({

        isClosed:monday.isClosed,

        openTime:monday.openTime,

        closeTime:monday.closeTime
      });
    }
  }

  //Submit

  submit():void{

    if(this.form.invalid){

      this.form.markAllAsTouched();

      return;
    }

    this.saving.set(true);

    const value=this.form.getRawValue();

    const id= this.restaurantId();

    const dto:RestaurantCreateDto ={

      name:value.name?? "",

      description:value.description?? "",

      cuisineTypeId:value.cusisneTypeId,

      imageUrl:value.imageUrl,

      isActive:value.isActive,

      address:value.address,

      restaurantTagIds:value.restaurantTagIds??[],

      restaurantId:value.restaurantId
    };

    const request$=id

    ?    this.restaurantService.update(id,dto)
    :this.restaurantService.create(dto);


    request$.subscribe({
      next:()=>{
        this.saving.set(false);;

        if(id){

          this.saveOperatingHours(id);

        }else{

          this.form.markAsPristine();
        }

        error:(err:any)=>{

          console.error('Restaurant save failed.', err);

          this.saving.set(false);
        }
      }

    });
  }



  private saveOperatingHours(restaurantId:number):void{

    const hours=this.operatingHours.getRawValue();

    //Each row can be sent to API; changed as ONE bulk endpoint

    for(const hour of hours){
      //this.hoursService.updateForRestaurant(restaurantId, hour).subscribe();
    }
  }

}
