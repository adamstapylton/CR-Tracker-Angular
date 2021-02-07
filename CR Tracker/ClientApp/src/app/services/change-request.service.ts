import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpParams, HttpResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

import { ChangeRequest } from '../models/changeRequest';
import { error } from 'protractor';

@Injectable({
  providedIn: 'root'
})

export class ChangeRequestService {
  private changeRequestUrl = 'api/changeRequests';

  constructor(private http: HttpClient) { }

  getChangeRequests(includeOnHold): Observable<ChangeRequest[]> {
    let params = new HttpParams().set('includeOnHold', includeOnHold)
    return this.http.get<ChangeRequest[]>(this.changeRequestUrl, {params: params})
      .pipe(
        tap(data => console.log(JSON.stringify(data)))
      );
  }

  addChangeRequest(changeRequest: ChangeRequest): Observable<ChangeRequest> {
    return this.http.post<ChangeRequest>(this.changeRequestUrl, changeRequest)
      .pipe(
        catchError(this.handleError)
      );

  }

  deleteChangeRequest(changeRequestId: string): Observable<{}> {
    return this.http.delete(`${this.changeRequestUrl}/${changeRequestId}`)
      .pipe(
      catchError(this.handleError)
    )
  }

  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      console.error('An error occurred:', error.error.message)
    } else if (error.error.title != undefined) {
      console.error(error.error.title)
      return throwError(error.error.title);
    } else {
      console.error(error.error)
      return throwError(error.error);
    }
    
  }
}
