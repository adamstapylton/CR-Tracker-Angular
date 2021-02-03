import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

import { ChangeRequest } from '../models/changeRequest';

@Injectable({
  providedIn: 'root'
})

export class ChangeRequestService {
  private changeRequestUrl = 'api/changerequests';

  constructor(private http: HttpClient) { }

  getChangeRequests(includeOnHold): Observable<ChangeRequest[]> {
    let params = new HttpParams().set('includeOnHold', includeOnHold)
    return this.http.get<ChangeRequest[]>(this.changeRequestUrl, {params: params})
      .pipe(
        tap(data => console.log(JSON.stringify(data)))
      );
  }
}
