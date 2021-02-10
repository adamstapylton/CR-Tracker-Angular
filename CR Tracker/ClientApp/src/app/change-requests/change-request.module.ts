import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { KanbanBoardComponent } from './kanban-board/kanban-board.component';
import { RouterModule } from '@angular/router';
import { CrFormComponent } from './cr-form/cr-form.component';
import { FormsModule } from '@angular/forms';
import { DetailsComponent } from './details/details.component';



@NgModule({
  declarations: [
    KanbanBoardComponent,
    CrFormComponent,
    DetailsComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule.forChild([
      { path: 'change-requests/kanban-board', component: KanbanBoardComponent },
      { path: 'change-requests/details/:id', component: DetailsComponent }
    ])
  ]
})
export class ChangeRequestModule { }
