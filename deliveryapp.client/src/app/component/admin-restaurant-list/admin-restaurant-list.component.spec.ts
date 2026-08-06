import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminRestaurantListComponent } from './admin-restaurant-list.component';

describe('AdminRestaurantListComponent', () => {
  let component: AdminRestaurantListComponent;
  let fixture: ComponentFixture<AdminRestaurantListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AdminRestaurantListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminRestaurantListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
