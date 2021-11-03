import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
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
  @ViewChild('audioOption') audioPlayerRef!: ElementRef;

  private subscription!: Subscription;

  displayedColumns: string[] = ['launch', 'preview', 'title'];
  voiceCommandsTable = new MatTableDataSource<VoiceCommand>();
  audioPreview: any;
  isVoiceCommandPlaying: boolean;
  isTableValid: boolean;

  constructor(private voiceCommandsHttp: VoiceCommandsHttpService) {
    this.isTableValid = true;
    this.isVoiceCommandPlaying = false;
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

  playAudioPreview(voiceCommand: any){
    // single audio element in DOM, change its src only if a different audio
    if (this.audioPlayerRef.nativeElement.getAttribute('audio-id') != voiceCommand.id){
      this.audioPlayerRef.nativeElement.setAttribute('src',this.getAudioPreviewFile(voiceCommand.id));
      this.audioPlayerRef.nativeElement.setAttribute('audio-id', voiceCommand.id);
    }
    voiceCommand.isPlaying = true;
    this.audioPlayerRef.nativeElement.play();
    this.audioPlayerRef.nativeElement.addEventListener('ended', () => {
      voiceCommand.isPlaying = false;
    });
  }

  getAudioPreviewFile(id: number){  
    return this.voiceCommandsHttp.getAudioPreview(id);
  }
}