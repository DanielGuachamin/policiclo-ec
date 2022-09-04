import { TestBed } from '@angular/core/testing';

import { ServCiclisService } from './serv-ciclis.service';

describe('ServCiclisService', () => {
  let service: ServCiclisService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ServCiclisService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
