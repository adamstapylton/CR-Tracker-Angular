import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { KanbanBoardComponent } from './kanban-board/kanban-board.component';
import { RouterModule } from '@angular/router';
import { CrFormComponent } from './cr-form/cr-form.component';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    KanbanBoardComponent,
    CrFormComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule.forChild([
      { path: 'change-requests/kanban-board', component: KanbanBoardComponent }
    ])
  ]
})
export class ChangeRequestModule { }
