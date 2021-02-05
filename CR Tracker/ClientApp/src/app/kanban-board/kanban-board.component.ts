import { Component, OnInit } from '@angular/core';
import { ChangeRequest } from '../models/changeRequest';
import { ChangeRequestService } from '../services/change-request.service';
import { Stage } from '../models/stage';
import { StageService } from '../services/stages.service';

@Component({
  selector: 'app-kanban-board',
  templateUrl: './kanban-board.component.html',
  styleUrls: ['./kanban-board.component.css']
})
export class KanbanBoardComponent implements OnInit {

  changeRequests: ChangeRequest[];
  stages: Stage[];
  errorMessage: string;
  stagesError: string;
  includeOnHold: boolean = true;

  constructor(private changeRequestService: ChangeRequestService, private stageService: StageService) { }

  ngOnInit(): void {
    this.updateChangeRequests();
    this.stageService.getStages().subscribe({
      next: stages => {
        this.stages = stages;
      },
      error: err => this.stagesError = err
    });
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
