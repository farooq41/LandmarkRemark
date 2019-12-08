import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MarkerSearchComponent } from './marker-search.component';

describe('MarkerSearchComponent', () => {
  let component: MarkerSearchComponent;
  let fixture: ComponentFixture<MarkerSearchComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MarkerSearchComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MarkerSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
