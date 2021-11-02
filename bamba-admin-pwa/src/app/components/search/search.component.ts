import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { VoiceCommandsHttpService } from '../../services/voiceCommandsHttp/voiceCommandsHttp.service';
import { Subscription } from 'rxjs';
import { VoiceCommand } from 'src/app/models/VoiceCommand';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {
  private subscription!: Subscription;

  displayedColumns: string[] = ['launch', 'preview', 'title'];
  voiceCommandsTable = new MatTableDataSource<VoiceCommand>();

  constructor(private voiceCommandsHttp: VoiceCommandsHttpService) { }

  ngOnInit() {
    this.voiceCommandsHttp.getAllActions().
    then(results => {
      this.voiceCommandsTable.data = results;
    })
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  activateVoiceCommand(id: number) {    
    this.voiceCommandsHttp.activateVoiceCommand(id);
  }
}