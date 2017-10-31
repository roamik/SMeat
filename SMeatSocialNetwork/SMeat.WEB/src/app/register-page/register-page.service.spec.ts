import { TestBed, inject } from '@angular/core/testing';

import { RegisterPageService } from './register-page.service';

describe('RegisterPageService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [RegisterPageService]
    });
  });

  it('should be created', inject([RegisterPageService], (service: RegisterPageService) => {
    expect(service).toBeTruthy();
  }));
});
