import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GroupListPageComponent } from './group-list-page.component';

describe('GroupListPageComponent', () => {
  let component: GroupListPageComponent;
  let fixture: ComponentFixture<GroupListPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GroupListPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GroupListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
