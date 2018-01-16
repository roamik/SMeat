import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkplaceModalComponent } from './workplace-modal.component';

describe('WorkplaceModalComponent', () => {
  let component: WorkplaceModalComponent;
  let fixture: ComponentFixture<WorkplaceModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WorkplaceModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WorkplaceModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
