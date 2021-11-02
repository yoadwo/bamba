import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { NgrokUrlService } from '../ngrokUrl/ngrok-url.service';


@Injectable({
  providedIn: 'root'
})
export class VoiceCommandsHttpService  {
  voiceCommandUrl: string = '';

  constructor(
    private http: HttpClient,
    private ngrok: NgrokUrlService) {
      var url = '';
      if (!environment.production){
        url = environment.baseUrl;
      } else {
        url = this.ngrok.getPublicTunnel();
      }
      this.voiceCommandUrl = url + '/api/voiceCommands';
     }

    

  async get(route: string, data?: any) {
    var url = this.voiceCommandUrl + route;
    let params = new HttpParams();

    if (data !== undefined) {
      Object.getOwnPropertyNames(data).forEach(key => {
        params = params.set(key, data[key]);
      });
    }

    const result = this.http.get(url, {
      responseType: 'json',
      params: params
    });

    return new Promise<any>((resolve, reject) => {
      result.subscribe(resolve as any, reject as any);
    });
  }

  getAllVoiceCommands():Promise<any> {
    return this.get('/');
  }

  getAudioPreview(id: number){
    //return this.get('/preview');
    return this.voiceCommandUrl + '/preview?id=' + id;
  }

  activateVoiceCommand(id: number){
    var url = this.voiceCommandUrl + '/activate';
    let search = new URLSearchParams();
    search.set('id', id.toString());
    this.http.post(url + '?' + search.toString(), {}).subscribe();
  }
}
