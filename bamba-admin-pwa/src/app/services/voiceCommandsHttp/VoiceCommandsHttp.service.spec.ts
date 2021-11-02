import { TestBed } from '@angular/core/testing';

import { VoiceCommandsHttpService } from './voiceCommandsHttp.service';

describe('VoiceCommandsHttpService', () => {
  let service: VoiceCommandsHttpService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(VoiceCommandsHttpService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
