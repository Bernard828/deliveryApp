import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrderTrackingComponentComponent } from './order-tracking-component.component';

describe('OrderTrackingComponentComponent', () => {
  let component: OrderTrackingComponentComponent;
  let fixture: ComponentFixture<OrderTrackingComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [OrderTrackingComponentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OrderTrackingComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
