import { TestBed } from '@angular/core/testing';

import { AddeditService } from './addedit.service';

describe('AddeditService', () => {
  let service: AddeditService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AddeditService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
