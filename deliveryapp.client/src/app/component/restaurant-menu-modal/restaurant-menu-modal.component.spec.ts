import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RestaurantMenuModalComponent } from './restaurant-menu-modal.component';

describe('RestaurantMenuModalComponent', () => {
  let component: RestaurantMenuModalComponent;
  let fixture: ComponentFixture<RestaurantMenuModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RestaurantMenuModalComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RestaurantMenuModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
