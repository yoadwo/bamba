import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { VoiceCommandsHttpService } from '../../services/voiceCommandsHttp/voiceCommandsHttp.service';
import { Subscription } from 'rxjs';
import { VoiceCommand } from 'src/app/models/VoiceCommand';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {
  private subscription!: Subscription;

  displayedColumns: string[] = ['launch', 'preview', 'title'];
  voiceCommandsTable = new MatTableDataSource<VoiceCommand>();
  audioPreview: any;
  isTableValid: boolean;

  constructor(private voiceCommandsHttp: VoiceCommandsHttpService) {
    this.isTableValid = true;
   }

  ngOnInit() {
    this.voiceCommandsHttp.getAllVoiceCommands().
    then(results => {
      this.isTableValid = true;
      this.voiceCommandsTable.data = results;
    })
    .catch(err => {
      this.isTableValid = false;
      console.error(err);
      this.voiceCommandsTable.data = [];
      
    })
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  activateVoiceCommand(id: number) {    
    this.voiceCommandsHttp.activateVoiceCommand(id);
  }

  PlayAudioPreview(id: number){
    return this.voiceCommandsHttp.getAudioPreview(id);
  }
}