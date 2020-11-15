import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ICustomTest } from './ICustomTest';

@Injectable({
  providedIn: 'root'
})
export class CustomTestService {

  private _url: string;

  constructor(private _httpClient: HttpClient) {
    this._url = `${environment.apiUrl}/tests`
  }

  createCustomTest(customTest: ICustomTest): Observable<ICustomTest> {
    return this._httpClient.post<ICustomTest>(this._url, customTest).pipe(catchError(this.handlePostPutCustomTestErrors));
  }

  getCustomTests(): Observable<ICustomTest[]> {
    return this._httpClient.get<ICustomTest[]>(this._url).pipe(catchError(this.logError));
  }

  getCustomTest(id: number): Observable<ICustomTest> {
    return this._httpClient.get<ICustomTest>(`${this._url}/${id}`).pipe(catchError(this.logError));
  }

  updateCustomTest(customTest: ICustomTest): Observable<void> {
    return this._httpClient.put<void>(`${this._url}/${customTest.id}`, customTest).pipe(catchError(this.handlePostPutCustomTestErrors));
  }

  deleteCustomTest(id: number): Observable<void> {
    return this._httpClient.delete<void>(`${this._url}/${id}`).pipe(catchError(this.logError));
  }

  private logError(errorResponse: HttpErrorResponse) {
    if (errorResponse.error instanceof ErrorEvent) {
      console.error("Client Side Error", errorResponse);
    } else {
      console.error("Server Side Error", errorResponse);
    }
    return throwError("There is a problem with the service. Please, try again later.");
  }

  private handlePostPutCustomTestErrors(errorResponse: HttpErrorResponse) {
    if (!(errorResponse.error instanceof ErrorEvent)) {
      return throwError(errorResponse.error);
    }
  }
}
