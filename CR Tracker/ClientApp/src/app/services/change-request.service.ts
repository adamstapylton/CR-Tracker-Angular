import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
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
    console.log('starting add cr request');
    return this.http.post<ChangeRequest>(this.changeRequestUrl, changeRequest).pipe(
      catchError(this.handleError),
      tap(data => console.log(JSON.stringify(data)))
    );

  }

  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      console.error('An error occurred:', error.error.message)
    } else {
      console.error(error.message)
    }
    return throwError(error.message);
  }
}
