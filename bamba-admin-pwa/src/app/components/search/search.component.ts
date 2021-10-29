import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { ActionsService } from '../../services/actions/actions.service';
import { Subscription } from 'rxjs';
import { Action } from 'src/app/models/action';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {
  private subscription!: Subscription;

  displayedColumns: string[] = ['title', 'preview', 'launch'];
  actions = new MatTableDataSource<Action>();

  constructor(private actionsService: ActionsService) { }

  ngOnInit() {
    this.searchActions();
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  async searchActions() {
    const results = await this.actionsService.getAllActions();
    this.actions.data = results;
  }

  launchSound(title: string) {    
    this.actionsService.launchAction(title);
  }
}