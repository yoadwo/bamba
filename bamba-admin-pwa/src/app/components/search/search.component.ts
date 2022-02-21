import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
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

  activateVoiceCommand(voiceCommand: VoiceCommand) {   
    voiceCommand.isPlaying = true; 
    this.voiceCommandsHttp.activateVoiceCommand(voiceCommand.id).subscribe((resp: any) => {
      if (resp.status){
        voiceCommand.isPlaying = false;
      }
    });
  }

  playAudioPreview(voiceCommand: VoiceCommand){
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
    this.audioPlayerRef.nativeElement.addEventListener('error', (event: Event) => {
      voiceCommand.isPlaying = false;
      //this.mapErrorByBrowser(event);
    });
  }

  mapErrorByBrowser(event: any){
    let error = event;

    // Chrome v60
    if (event.path && event.path[0]) {
      error = event.path[0].error;
    }

    // // Firefox v55
    if (event.originalTarget) {
      error = error.originalTarget.error;
    }

    // // Here comes the error message
    let errMessage;
    if (error.message != ""){
      errMessage = error.message;
    }
    else {
      errMessage = "שגיאה במציאת הקובץ על מחשב היעד"
    }
    alert(errMessage);

  }

  getAudioPreviewFile(id: number){  
    return this.voiceCommandsHttp.getAudioPreview(id);
  }
}