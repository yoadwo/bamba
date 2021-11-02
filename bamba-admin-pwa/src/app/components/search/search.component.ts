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

  displayedColumns: string[] = ['launch', 'preview', 'title'];
  actions = new MatTableDataSource<Action>();

  constructor(private actionsService: ActionsService) { }

  ngOnInit() {
    this.actionsService.getAllActions().
    then(results => {
      this.actions.data = results;
    })
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  launchSound(id: number) {    
    this.actionsService.launchAction(id);
  }
}