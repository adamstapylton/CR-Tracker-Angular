import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Worktype } from "../models/worktype";


@Injectable({
  providedIn: 'root'
})

export class WorkTypeService {
  private worktypeUrl = 'api/worktypes';

  constructor(private http: HttpClient) { }

  getWorktypes(): Observable<Worktype[]> {
    return this.http.get<Worktype[]>(this.worktypeUrl);
  }
}
