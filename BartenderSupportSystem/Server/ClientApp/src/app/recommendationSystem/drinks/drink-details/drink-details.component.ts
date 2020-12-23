import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import Swal from 'sweetalert2';
import { BrandService } from '../../brands/brand/brand.service';
import { DrinkService } from '../drink/drink.service';
import { IDrink } from '../drink/IDrink';
import { CountryConverter } from '../../brands/brand/CountryConverter';
import { IBrand } from '../../brands/brand/IBrand';

@Component({
  selector: 'app-drink-details',
  templateUrl: './drink-details.component.html',
  styleUrls: ['./drink-details.component.css'],
})
export class DrinkDetailsComponent implements OnInit {
  drink: IDrink;
  brand: IBrand;

  constructor(
    private _brandService: BrandService,
    private _drinkService: DrinkService,
    private _activatedRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this._activatedRoute.paramMap.subscribe(
      (params) => {
        const id = +params.get('id');
        this._drinkService.getDrink(id).subscribe(
          (data) => {
            this.drink = data;
            this._brandService.getBrand(data.brandId).subscribe(
              (data) => {
                this.brand = data;
                this.drink.brand = this.brand;
                this.drink.brand.countryOfOrigin = CountryConverter.fromCodeToName(
                  this.brand.countryOfOrigin
                );
              },
              () => {
                Swal.fire({
                  position: 'center',
                  icon: 'error',
                  title: 'Oops...',
                  text: 'Something went wrong!',
                });
              }
            );
          },
          () => {
            Swal.fire({
              position: 'center',
              icon: 'error',
              title: 'Oops...',
              text: 'Something went wrong!',
            });
          }
        );
      },
      () => {
        Swal.fire({
          position: 'center',
          icon: 'error',
          title: 'Oops...',
          text: 'Something went wrong!',
        });
      }
    );
  }
}
