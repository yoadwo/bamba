import { Component, OnInit } from '@angular/core';
import { NgrokUrlService } from 'src/app/services/ngrokUrl/ngrok-url.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  url: string = '';

  constructor(private ngrokUrl: NgrokUrlService) { }

  ngOnInit(): void {
    if (!environment.production){
      this.url += environment.baseUrl;
    } else {
      this.url += this.ngrokUrl.getPublicTunnel();
    }
  }

}
