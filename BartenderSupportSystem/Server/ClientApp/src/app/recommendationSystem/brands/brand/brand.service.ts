import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { IBrand } from './IBrand';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class BrandService {
  private _url: string;

  constructor(private _httpClient: HttpClient) {
    this._url = `${environment.apiUrl}/brands`;
  }

  createBrand(brand: IBrand): Observable<IBrand> {
    return this._httpClient
      .post<IBrand>(this._url, brand)
      .pipe(catchError(this.logError));
  }

  getBrands(): Observable<IBrand[]> {
    return this._httpClient
      .get<IBrand[]>(this._url)
      .pipe(catchError(this.logError));
  }

  getBrand(id: number): Observable<IBrand> {
    return this._httpClient
      .get<IBrand>(`${this._url}/${id}`)
      .pipe(catchError(this.logError));
  }

  updateBrand(brand: IBrand): Observable<void> {
    return this._httpClient
      .put<void>(`${this._url}/${brand.id}`, brand)
      .pipe(catchError(this.logError));
  }

  deleteBrand(id: number): Observable<void> {
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
