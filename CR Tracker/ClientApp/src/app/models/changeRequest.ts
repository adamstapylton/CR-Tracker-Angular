import { Note } from "./Note";
import { Stage } from "./stage";
import { Worktype } from "./worktype";

export class ChangeRequest {
  changeRequestId: string;
  description: string;
  dateRaised: Date;
  dateRequired: Date;
  assignedToUserId: number;
  raisedByUserId: number;
  worktypes: Worktype[];
  billingRulesRequired: boolean;
  onHold: boolean;
  notes: Note[];
  stage: Stage;

}
