import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import * as NgrokFile from '../../../assets/tunnels.json';

@Injectable({
  providedIn: 'root'
})
export class NgrokUrlService {

  constructor() { }

  getPublicTunnel(): string{
    var ngrok = NgrokFile as NgrokModel;
    var httpsTunnel = ngrok.tunnels.find(tunnel => tunnel.public_url.includes("https"));
    if (httpsTunnel == null) throw new Error('Could not get tunnel address from tunnels.json');
    return httpsTunnel.public_url;
  }
}

interface NgrokModel{
  tunnels: Tunnel[]
  uri: string
}

interface Tunnel{
  public_url: string;
}