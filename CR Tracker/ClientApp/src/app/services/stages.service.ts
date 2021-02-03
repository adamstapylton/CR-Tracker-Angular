import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

import { Stage } from '../models/stage';

@Injectable({
  providedIn: 'root'
})

export class StageService {
  private stageUrl = 'api/stages'

  constructor(private http: HttpClient) { }

  getStages(): Observable<Stage[]> {
    return this.http.get<Stage[]>(this.stageUrl);
  }
}
