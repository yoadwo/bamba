import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import * as Tunnels from '../../../assets/tunnels.json';

//const baseUrl = 'http://localhost:61839/api/actions';
//const baseUrl = 'http://2bfa-77-125-32-61.ngrok.io/api/actions';

@Injectable({
  providedIn: 'root'
})
export class ActionsService {

  constructor(private http: HttpClient) { }

  async get(route: string, data?: any) {
    const url = environment.baseUrl + route;
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

  searchActions(query: string):Promise<any> {
    return this.get('/search', {title: query});
  }

  getAllActions():Promise<any> {
    return this.get('/getAll');
  }

  launchAction(title: string){
    let url = `${environment.baseUrl}/launch`;
  let search = new URLSearchParams();
  search.set('title', title);
  this.http.post(url + '?' + search.toString(), {}).subscribe();
  }
}
