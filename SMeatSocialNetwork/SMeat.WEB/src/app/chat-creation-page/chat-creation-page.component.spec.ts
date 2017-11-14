import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ChatCreationPageComponent } from './chat-creation-page.component';

describe('ChatCreationPageComponent', () => {
  let component: ChatCreationPageComponent;
  let fixture: ComponentFixture<ChatCreationPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ChatCreationPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ChatCreationPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
