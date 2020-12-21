import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';
import { ICocktailFilter } from '../cocktail-filter/ICocktailFilter';
import { CocktailService } from '../cocktail/cocktail.service';
import { ICocktail } from '../cocktail/ICocktail';

@Component({
  selector: 'app-cocktail-list',
  templateUrl: './cocktail-list.component.html',
  styleUrls: ['./cocktail-list.component.css'],
})
export class CocktailListComponent implements OnInit {
  cocktails: ICocktail[];
  filteredCocktails: ICocktail[];
  statusMessage = 'Loading...';

  constructor(
    private _cocktailService: CocktailService,
    private activatedRoute: ActivatedRoute,
    private _router: Router
  ) {}

  ngOnInit(): void {
    this._cocktailService.getCocktails().subscribe(
      (data) => {
        if (data.length === 0) {
          this.statusMessage = 'No cocktails to display.';
        } else {
          this.cocktails = data;
          this.filteredCocktails = this.cocktails;
        }
      },
      (error) => {
        this.statusMessage = error;
      }
    );
  }

  editCocktail(id: number): void {
    this._router.navigate(['/cocktails/edit', id]);
  }

  deleteCocktail(cocktail: ICocktail): void {
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!',
    }).then((result: { isConfirmed: boolean }) => {
      if (result.isConfirmed) {
        this._cocktailService.deleteCocktail(cocktail.id).subscribe(
          () => {
            const cocktailIndex: number = this.cocktails.indexOf(cocktail, 0);
            this.cocktails.splice(cocktailIndex, 1);
            this.filteredCocktails = this.cocktails;
          },
          (error: any) => console.log(error)
        );
        Swal.fire('Deleted!', 'Your record has been deleted.', 'success');
      }
    });
  }

  filterCocktails(cocktailFilterModel: ICocktailFilter): void {
    if (cocktailFilterModel != null) {
      this.filteredCocktails = this.cocktails;
      if (cocktailFilterModel.description != '') {
        this.filteredCocktails = this.filteredCocktails.filter((e) =>
          e.description
            .toLowerCase()
            .includes(cocktailFilterModel.description.toLowerCase())
        );
      }
      if (cocktailFilterModel.name != '') {
        this.filteredCocktails = this.filteredCocktails.filter((e) =>
          e.name.toLowerCase().includes(cocktailFilterModel.name.toLowerCase())
        );
      }
      if (cocktailFilterModel.cocktailType != '') {
        this.filteredCocktails = this.filteredCocktails.filter(
          (e) =>
            e.cocktailType.toLowerCase() ===
            cocktailFilterModel.cocktailType.toLowerCase()
        );
      }
    } else {
      this.filteredCocktails = this.cocktails;
    }
  }
}
