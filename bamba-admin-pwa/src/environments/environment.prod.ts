import * as Tunnels from '../assets/tunnels.json';

function getPublicTunnel(){
  var httpsTunnel = Tunnels.tunnels.find(t => t.public_url.includes("https"));
  return httpsTunnel?.public_url;
}

export const environment = {
  production: true,
  baseUrl: 'https://05fc-77-125-32-61.ngrok.io' + 'api/actions'
};
