import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { IDrinkFilter } from '../drink-filter/IDrinkFilter';
import { DrinkService } from '../drink/drink.service';
import { IDrink } from '../drink/IDrink';

@Component({
  selector: 'app-drink-list',
  templateUrl: './drink-list.component.html',
  styleUrls: ['./drink-list.component.css'],
})
export class DrinkListComponent implements OnInit {
  drinks: IDrink[];
  filteredDrinks: IDrink[];
  statusMessage = 'Loading...';

  constructor(private _drinkService: DrinkService, private _router: Router) {}

  ngOnInit(): void {
    this._drinkService.getDrinks().subscribe(
      (data) => {
        if (data.length === 0) {
          this.statusMessage = 'No drinks to display.';
        } else {
          this.drinks = data;
          this.filteredDrinks = this.drinks;
        }
      },
      (error) => {
        this.statusMessage = error;
      }
    );
  }

  editDrink(id: number): void {
    this._router.navigate(['/drinks/edit', id]);
  }

  deleteDrink(drink: IDrink): void {
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
        this._drinkService.deleteDrink(drink.id).subscribe(
          () => {
            const drinkIndex: number = this.drinks.indexOf(drink, 0);
            this.drinks.splice(drinkIndex, 1);
            this.filteredDrinks = this.drinks;
          },
          (error) => console.log(error)
        );
        Swal.fire('Deleted!', 'Your record has been deleted.', 'success');
      }
    });
  }

  filterDrinks(drinkFilterModel: IDrinkFilter): void {
    if (drinkFilterModel != null) {
      this.filteredDrinks = this.drinks;
      if (drinkFilterModel.flavor != '') {
        this.filteredDrinks = this.filteredDrinks.filter((e) =>
          e.flavor.toLowerCase().includes(drinkFilterModel.flavor.toLowerCase())
        );
      }
      if (drinkFilterModel.name != '') {
        this.filteredDrinks = this.filteredDrinks.filter((e) =>
          e.name.toLowerCase().includes(drinkFilterModel.name.toLowerCase())
        );
      }
      if (drinkFilterModel.alcoholType != '') {
        this.filteredDrinks = this.filteredDrinks.filter(
          (e) =>
            e.alcoholType.toLowerCase() ===
            drinkFilterModel.alcoholType.toLowerCase()
        );
      }
      if (drinkFilterModel.brandId != 0) {
        this.filteredDrinks = this.filteredDrinks.filter(
          (e) => e.brandId === drinkFilterModel.brandId
        );
      }
      if (!drinkFilterModel.isNonAlcohol) {
        const minVal: number = drinkFilterModel.alcoholPercentageMinValue;
        const maxVal: number = drinkFilterModel.alcoholPercentageMaxValue;
        if (minVal > maxVal) {
          this.filteredDrinks = this.filteredDrinks.filter(
            (e) => e.alcoholPercentage >= minVal
          );
        }
        if (minVal == maxVal && maxVal != 0) {
          this.filteredDrinks = this.filteredDrinks.filter(
            (e) => e.alcoholPercentage <= maxVal
          );
        }
        if (minVal < maxVal) {
          this.filteredDrinks = this.filteredDrinks.filter(
            (e) =>
              e.alcoholPercentage <= maxVal && e.alcoholPercentage >= minVal
          );
        }
      }
      if (drinkFilterModel.isNonAlcohol) {
        this.filteredDrinks = this.filteredDrinks.filter(
          (e) => e.alcoholPercentage == 0
        );
      }
      if (drinkFilterModel.pricePerMl != '') {
        if (drinkFilterModel.pricePerMl === '1') {
          this.filteredDrinks = this.filteredDrinks.sort((a, b) => {
            if (a.pricePerMl < b.pricePerMl) {
              return -1;
            }
            if (a.pricePerMl > b.pricePerMl) {
              return 1;
            }
            return 0;
          });
        } else {
          this.filteredDrinks = this.filteredDrinks.sort((a, b) => {
            if (a.pricePerMl > b.pricePerMl) {
              return -1;
            }
            if (a.pricePerMl < b.pricePerMl) {
              return 1;
            }
            return 0;
          });
        }
      } else {
        this.filteredDrinks = this.filteredDrinks.sort((a, b) => {
          if (a.id < b.id) {
            return -1;
          }
          if (a.id > b.id) {
            return 1;
          }
          return 0;
        });
      }
    } else {
      this.filteredDrinks = this.drinks;
    }
  }
}
