import {
  HttpClient,
  HttpErrorResponse,
  HttpParams,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ICustomTestResult } from './ICustomTestResult';

@Injectable({
  providedIn: 'root',
})
export class CustomTestResultService {
  private _url: string;

  constructor(private _httpClient: HttpClient) {
    this._url = `${environment.apiUrl}/customTestResults`;
  }

  createCustomTestResult(
    customTest: ICustomTestResult
  ): Observable<ICustomTestResult> {
    return this._httpClient
      .post<ICustomTestResult>(this._url, customTest)
      .pipe(catchError(this.logError));
  }

  getUserCustomTestResults(
    username: string,
    testId?: number
  ): Observable<ICustomTestResult[]> {
    let params: HttpParams = new HttpParams({
      fromObject: { username: username },
    });
    if (testId) {
      params = new HttpParams({
        fromObject: {
          username: username,
          testId: testId.toString(),
        },
      });
    }
    return this._httpClient
      .get<ICustomTestResult[]>(this._url, { params: params })
      .pipe(catchError(this.logError));
  }

  private logError(errorResponse: HttpErrorResponse) {
    if (errorResponse.error instanceof ErrorEvent) {
      console.error('Client Side Error', errorResponse);
    } else {
      console.error('Server Side Error', errorResponse);
    }
    return throwError(
      'There is a problem with the service. Please, try again later.'
    );
  }
}
