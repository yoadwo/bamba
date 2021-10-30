import { TestBed } from '@angular/core/testing';

import { NgrokUrlService } from './ngrok-url.service';

describe('NgrokUrlService', () => {
  let service: NgrokUrlService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NgrokUrlService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
