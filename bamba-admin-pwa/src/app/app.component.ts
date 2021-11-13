import { Component } from '@angular/core';
import { NgrokUrlService } from './services/ngrokUrl/ngrok-url.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'כלבלב בשלט רחוק';

  constructor() {
  }
}