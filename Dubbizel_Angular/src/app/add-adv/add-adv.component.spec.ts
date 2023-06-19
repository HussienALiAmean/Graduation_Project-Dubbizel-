import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddAdvComponent } from './add-adv.component';

describe('AddAdvComponent', () => {
  let component: AddAdvComponent;
  let fixture: ComponentFixture<AddAdvComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddAdvComponent]
    });
    fixture = TestBed.createComponent(AddAdvComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
