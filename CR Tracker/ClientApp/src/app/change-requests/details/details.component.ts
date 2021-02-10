import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { ChangeRequest } from '../../models/changeRequest';
import { ChangeRequestService } from '../../services/change-request.service';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {

  constructor(private route: ActivatedRoute, private changeRequestService: ChangeRequestService) {
    this.changeRequestId = this.route.snapshot.paramMap.get('id');
    console.log(this.changeRequestId);
  }

  changeRequestId: string;
  changeRequest: ChangeRequest;
  errorMessage: string;

  ngOnInit() {
    this.getChangeRequest();
  }

  getChangeRequest() {
    this.changeRequestService.getChangeRequestById(this.changeRequestId)
      .subscribe({
        next: changeRequest => {
          this.changeRequest = changeRequest;
        },
        error: err => {
          this.errorMessage = err;
        }
      });
  }

}
