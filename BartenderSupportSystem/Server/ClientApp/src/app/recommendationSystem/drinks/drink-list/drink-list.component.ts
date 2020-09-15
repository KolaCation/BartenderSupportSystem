import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';
import { DrinkService } from '../drink/drink.service';
import { IDrink } from '../drink/IDrink';

@Component({
  selector: 'app-drink-list',
  templateUrl: './drink-list.component.html',
  styleUrls: ['./drink-list.component.css']
})
export class DrinkListComponent implements OnInit {

  drinks: IDrink[];
  statusMessage: string = "Loading...";

  constructor(private _drinkService: DrinkService, private activatedRoute: ActivatedRoute, private _router: Router) { }

  ngOnInit(): void {
    this._drinkService.getDrinks().subscribe(
      data => {
        if (data.length === 0) {
          this.statusMessage = "No drinks to display."
        } else {
          this.drinks = data;
        }
      },
      error => {
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
      confirmButtonText: 'Yes, delete it!'
    }).then((result: { isConfirmed: boolean; }) => {
      if (result.isConfirmed) {
        this._drinkService.deleteDrink(drink.id).subscribe(
          () => {
            let drinkIndex: number = this.drinks.indexOf(drink, 0);
            this.drinks.splice(drinkIndex, 1);
          },
          (error: any) => console.log(error)
        );
        Swal.fire(
          'Deleted!',
          'Your record has been deleted.',
          'success'
        );
      }
    });
  }
}
