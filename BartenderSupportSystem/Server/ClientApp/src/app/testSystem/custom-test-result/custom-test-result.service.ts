import { HttpClient, HttpErrorResponse } from '@angular/common/http';
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

  getCustomTestResults(): Observable<ICustomTestResult[]> {
    return this._httpClient
      .get<ICustomTestResult[]>(this._url)
      .pipe(catchError(this.logError));
  }

  getCustomTestResult(id: number): Observable<ICustomTestResult> {
    return this._httpClient
      .get<ICustomTestResult>(`${this._url}/${id}`)
      .pipe(catchError(this.logError));
  }

  updateCustomTestResult(customTest: ICustomTestResult): Observable<void> {
    return this._httpClient
      .put<void>(`${this._url}/${customTest.id}`, customTest)
      .pipe(catchError(this.logError));
  }

  deleteCustomTestResult(id: number): Observable<void> {
    return this._httpClient
      .delete<void>(`${this._url}/${id}`)
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
