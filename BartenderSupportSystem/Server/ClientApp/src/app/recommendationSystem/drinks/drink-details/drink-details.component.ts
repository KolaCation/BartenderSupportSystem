import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';
import { BrandService } from '../../brands/brand/brand.service';
import { DrinkService } from '../drink/drink.service';
import { IDrink } from '../drink/IDrink';
import { CountryConverter } from '../../brands/brand/CountryConverter';

@Component({
  selector: 'app-drink-details',
  templateUrl: './drink-details.component.html',
  styleUrls: ['./drink-details.component.css']
})
export class DrinkDetailsComponent implements OnInit {

  drink: IDrink;

  constructor(private _brandService: BrandService, private _drinkService: DrinkService,
    private _activatedRoute: ActivatedRoute, private _router: Router) { }

  ngOnInit(): void {
    this._activatedRoute.paramMap.subscribe(
      params => {
        const id = +params.get('id');
        this._drinkService.getDrink(id).subscribe(
          data => {
            this.drink = data;
            this._brandService.getBrand(data.brandId).subscribe(
              data => {
                this.drink.brand = data;
                this.drink.brand.countryOfOrigin = CountryConverter.fromCodeToName(data.countryOfOrigin);
              },
              err => {
                console.log(err);
                Swal.fire({
                  position: 'center',
                  icon: 'error',
                  title: 'Oops...',
                  text: 'Something went wrong!'
                });
              }
            );
          },
          err => {
            console.log(err);
            Swal.fire({
              position: 'center',
              icon: 'error',
              title: 'Oops...',
              text: 'Something went wrong!'
            });
          }
        );
      },
      err => {
        console.log(err);
        Swal.fire({
          position: 'center',
          icon: 'error',
          title: 'Oops...',
          text: 'Something went wrong!'
        });
      }
    );
  }


}
