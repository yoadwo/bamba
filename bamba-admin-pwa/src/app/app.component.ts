import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Bamba-admin-pwa';
  url: string = '';
  searchForm!: FormGroup;

  constructor(private formBuilder: FormBuilder,
              private router: Router) {
                //this.searchForm = new FormGroup({});
  }

  ngOnInit(): void {
    this.searchForm = this.formBuilder.group({
      search: ['', Validators.required],
    });
    this.url += environment.baseUrl;
  }

  onSearch(): void {
    if (!this.searchForm.valid) { return; }
    this.router.navigate(['search'], {      
       queryParams: {query: this.searchForm?.get('search')?.value}
      });
  }
}