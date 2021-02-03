import { Component, OnInit } from '@angular/core';
import { ChangeRequest } from '../models/changeRequest';
import { ChangeRequestService } from '../services/change-request.service';

@Component({
  selector: 'app-kanban-board',
  templateUrl: './kanban-board.component.html',
  styleUrls: ['./kanban-board.component.css']
})
export class KanbanBoardComponent implements OnInit {

  changeRequests: ChangeRequest[];
  errorMessage: string;
  includeOnHold: boolean = true; 

  constructor(private changeRequestService: ChangeRequestService) { }

  ngOnInit(): void {
    this.updateChangeRequests();
  };

  updateChangeRequests():void {
    this.changeRequestService.getChangeRequests(this.includeOnHold).subscribe({
      next: changeRequests => {
        this.changeRequests = changeRequests;
      },
      error: err => this.errorMessage = err
    });
  }

  changeOnHold(): void {
    this.updateChangeRequests();
  }


}
