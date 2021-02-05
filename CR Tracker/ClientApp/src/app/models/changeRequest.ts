import { Note } from "./Note";
import { Stage } from "./stage";
import { User } from "./user";
import { Worktype } from "./worktype";

export class ChangeRequest {
  changeRequestId: string;
  description: string;
  dateRequired: Date;
  assignedToUser: User;
  raisedByUser: User;
  worktypes: Worktype[];
  billingRulesRequired: boolean;
  onHold: boolean;
  notes: Note[];
  stage: Stage;

}
