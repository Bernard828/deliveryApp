import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminRestaurantDetailComponent } from './admin-restaurant-detail.component';

describe('AdminRestaurantDetailComponent', () => {
  let component: AdminRestaurantDetailComponent;
  let fixture: ComponentFixture<AdminRestaurantDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AdminRestaurantDetailComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminRestaurantDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
