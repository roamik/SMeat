import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GroupCreationPageComponent } from './group-creation-page.component';

describe('GroupCreationPageComponent', () => {
  let component: GroupCreationPageComponent;
  let fixture: ComponentFixture<GroupCreationPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GroupCreationPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GroupCreationPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
