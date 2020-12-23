import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { ICocktail } from './ICocktail';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class CocktailService {
  private _url: string;

  constructor(private _httpClient: HttpClient) {
    this._url = `${environment.apiUrl}/cocktails`;
  }

  createCocktail(cocktail: ICocktail): Observable<ICocktail> {
    return this._httpClient
      .post<ICocktail>(this._url, cocktail)
      .pipe(catchError(this.logError));
  }

  getCocktails(): Observable<ICocktail[]> {
    return this._httpClient
      .get<ICocktail[]>(this._url)
      .pipe(catchError(this.logError));
  }

  getCocktail(id: number): Observable<ICocktail> {
    return this._httpClient
      .get<ICocktail>(`${this._url}/${id}`)
      .pipe(catchError(this.logError));
  }

  updateCocktail(cocktail: ICocktail): Observable<void> {
    return this._httpClient
      .put<void>(`${this._url}/${cocktail.id}`, cocktail)
      .pipe(catchError(this.logError));
  }

  deleteCocktail(id: number): Observable<void> {
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
