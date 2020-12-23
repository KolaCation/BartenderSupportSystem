import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import Swal from 'sweetalert2';
import { CocktailService } from '../cocktail/cocktail.service';
import { ICocktail } from '../cocktail/ICocktail';

@Component({
  selector: 'app-cocktail-details',
  templateUrl: './cocktail-details.component.html',
  styleUrls: ['./cocktail-details.component.css'],
})
export class CocktailDetailsComponent implements OnInit {
  cocktail: ICocktail;
  componentList: any[] = [];
  isAdmin = false;

  constructor(
    private _cocktailService: CocktailService,
    private _activatedRoute: ActivatedRoute,
    private _authorizeService: AuthorizeService
  ) {}

  ngOnInit(): void {
    this._authorizeService.getUserRole().subscribe(
      (role) => {
        this.isAdmin = role.toLowerCase() === 'admin';
        this._activatedRoute.paramMap.subscribe((params) => {
          const id = +params.get('id');
          this._cocktailService.getCocktail(id).subscribe(
            (data) => {
              this.cocktail = data;
              this.cocktail.ingredients.forEach((e) =>
                this.componentList.push(e)
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
        });
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
