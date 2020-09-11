import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { IDrink } from './IDrink';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class DrinkService {

  private _url: string;

  constructor(private _httpClient: HttpClient) {
    this._url = `${environment.apiUrl}/drinks`
  }

  createDrink(drink: IDrink): Observable<IDrink> {
    return this._httpClient.post<IDrink>(this._url, drink).pipe(catchError(this.logError));
  }

  getDrinks(): Observable<IDrink[]> {
    return this._httpClient.get<IDrink[]>(this._url).pipe(catchError(this.logError));
  }

  getDrink(id: number): Observable<IDrink> {
    return this._httpClient.get<IDrink>(`${this._url}/${id}`).pipe(catchError(this.logError));
  }

  updateDrink(drink: IDrink): Observable<void> {
    return this._httpClient.put<void>(`${this._url}/${drink.id}`, drink).pipe(catchError(this.logError));
  }

  deleteDrink(id: number): Observable<void> {
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
}
