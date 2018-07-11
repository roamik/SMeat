import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ContactsPickerModalComponent } from './contacts-picker-modal.component';

describe('ContactsPickerModalComponent', () => {
  let component: ContactsPickerModalComponent;
  let fixture: ComponentFixture<ContactsPickerModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ContactsPickerModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ContactsPickerModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
