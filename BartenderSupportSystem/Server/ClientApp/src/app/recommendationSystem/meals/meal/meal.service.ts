import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { IMeal } from './IMeal';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class MealService {

  private _url: string;

  constructor(private _httpClient: HttpClient) {
    this._url = `${environment.apiUrl}/meals`
  }

  createMeal(meal: IMeal): Observable<IMeal> {
    return this._httpClient.post<IMeal>(this._url, meal).pipe(catchError(this.handlePostPutMealErrors));
  }

  getMeals(): Observable<IMeal[]> {
    return this._httpClient.get<IMeal[]>(this._url).pipe(catchError(this.logError));
  }

  getMeal(id: number): Observable<IMeal> {
    return this._httpClient.get<IMeal>(`${this._url}/${id}`).pipe(catchError(this.logError));
  }

  updateMeal(meal: IMeal): Observable<void> {
    return this._httpClient.put<void>(`${this._url}/${meal.id}`, meal).pipe(catchError(this.handlePostPutMealErrors));
  }

  deleteMeal(id: number): Observable<void> {
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

  private handlePostPutMealErrors(errorResponse: HttpErrorResponse) {
    if(!(errorResponse.error instanceof ErrorEvent)) {
      return throwError(errorResponse.error);
    }
  }
}
