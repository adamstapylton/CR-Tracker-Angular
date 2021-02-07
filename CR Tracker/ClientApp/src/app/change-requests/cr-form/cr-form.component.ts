import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { ChangeRequest } from '../../models/changeRequest';
import { Stage } from '../../models/stage';
import { User } from '../../models/user';
import { Worktype } from '../../models/worktype';
import { ChangeRequestService } from '../../services/change-request.service';
import { UserService } from '../../services/user.service';
import { WorkTypeService } from '../../services/worktype.service';

@Component({
  selector: 'app-cr-form',
  templateUrl: './cr-form.component.html',
  styleUrls: ['./cr-form.component.css']
})
export class CrFormComponent implements OnInit {

  constructor(private worktypeService: WorkTypeService,
    private userService: UserService,
    private changeRequestService: ChangeRequestService) {

  }

  @Output() changeRequestAdded: EventEmitter<any> = new EventEmitter();
  newChangeRequest: ChangeRequest;
  worktypes: Worktype[];
  users: User[];
  department: string = 'PSG';
  httpError: string;

  ngOnInit() {
    this.newChangeRequest = new ChangeRequest

     this.worktypeService.getWorktypes().subscribe({
      next: worktypes => {
        this.worktypes = worktypes;
      },
      error: err => console.log(err)
     });

    this.userService.getUsers().subscribe({
      next: users => {
        this.users = users;
      },
      error: err => console.log(err)
    })
  }

  onSubmit(): void{
    this.newChangeRequest.changeRequestId = `PSG${this.newChangeRequest.changeRequestId}`;
    this.changeRequestService.addChangeRequest(this.newChangeRequest).subscribe(
      (addedChangeRequest) => this.onSuccess(addedChangeRequest),
      err => this.httpError = err
    );
  }

  onSuccess(data): void {
    this.changeRequestAdded.emit(null);
    closeModal('#addCrModal');
    this.newChangeRequest = new ChangeRequest();
    this.httpError = null;
  }

}
