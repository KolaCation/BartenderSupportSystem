import { Component, OnInit } from '@angular/core';
import { MealService } from '../meal/meal.service';
import { Router } from '@angular/router';
import { IMeal } from '../meal/IMeal';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-meal-list',
  templateUrl: './meal-list.component.html',
  styleUrls: ['./meal-list.component.css'],
})
export class MealListComponent implements OnInit {
  meals: IMeal[];
  statusMessage = 'Loading...';

  constructor(private _mealService: MealService, private _router: Router) {}

  ngOnInit(): void {
    this._mealService.getMeals().subscribe(
      (data) => {
        if (data.length === 0) {
          this.statusMessage = 'No meals to display.';
        } else {
          this.meals = data;
        }
      },
      (error) => {
        this.statusMessage = error;
      }
    );
  }

  editMeal(id: number): void {
    this._router.navigate(['/meals/edit', id]);
  }

  deleteMeal(meal: IMeal): void {
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
        this._mealService.deleteMeal(meal.id).subscribe(
          () => {
            const mealIndex: number = this.meals.indexOf(meal, 0);
            this.meals.splice(mealIndex, 1);
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
        Swal.fire('Deleted!', 'Your record has been deleted.', 'success');
      }
    });
  }
}
