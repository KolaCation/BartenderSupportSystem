import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';
import { CocktailService } from '../cocktail/cocktail.service';
import { ICocktail } from '../cocktail/ICocktail';

@Component({
  selector: 'app-cocktail-details',
  templateUrl: './cocktail-details.component.html',
  styleUrls: ['./cocktail-details.component.css']
})
export class CocktailDetailsComponent implements OnInit {

  cocktail: ICocktail;
  componentList: any[] = new Array();

  constructor(private _cocktailService: CocktailService,
    private _activatedRoute: ActivatedRoute, private _router: Router) { }

  ngOnInit(): void {
    this._activatedRoute.paramMap.subscribe(
      params => {
        const id = +params.get('id');
        this._cocktailService.getCocktail(id).subscribe(
          data => {
            this.cocktail = data;
            this.cocktail.ingredients.forEach(e => this.componentList.push(e));
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
    );
  }
}
