import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowAddvertisComponent } from './show-addvertis.component';

describe('ShowAddvertisComponent', () => {
  let component: ShowAddvertisComponent;
  let fixture: ComponentFixture<ShowAddvertisComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ShowAddvertisComponent]
    });
    fixture = TestBed.createComponent(ShowAddvertisComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
