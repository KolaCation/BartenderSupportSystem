import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IBrand } from './IBrand';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BrandService {

  private _url : string;

  constructor(private _httpClient : HttpClient) {
    this._url = `${environment.apiUrl}/brands`
   }

  createBrand(brand : IBrand) : Observable<number> {
    return this._httpClient.post<number>(this._url, brand);
  }
}
