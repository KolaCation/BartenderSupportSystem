import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { IMeal } from '../meal/IMeal';
import { MealService } from '../meal/meal.service';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';
import { ErrorHandlerService } from '../../../shared/ErrorHandlerService';
import { AuthorizeService } from 'src/api-authorization/authorize.service';

@Component({
  selector: 'app-meal-form',
  templateUrl: './meal-form.component.html',
  styleUrls: ['./meal-form.component.css'],
})
export class MealFormComponent implements OnInit {
  mealForm: FormGroup;
  meal: IMeal;
  isAdmin = false;

  messages = {
    name: {
      minlength: 'Name must be at least 2 chars long.',
      maxlength: 'Name must not exceed 60 chars.',
      required: 'Name is required.',
    },
    pricePerGr: {
      required: 'Price per gram is required.',
      min: 'Min value: 0.',
      max: 'Max value: 10000.',
    },
  };

  formErrors = {
    name: '',
    pricePerGr: '',
    mealType: '',
  };

  constructor(
    private _formBuilder: FormBuilder,
    private _mealService: MealService,
    private _activatedRoute: ActivatedRoute,
    private _router: Router,
    private _errService: ErrorHandlerService,
    private _authorizeService: AuthorizeService
  ) {}

  ngOnInit(): void {
    this._authorizeService.getUserRole().subscribe(
      (role) => {
        this.isAdmin = role.toLowerCase() === 'admin';
        if (!this.isAdmin) this._router.navigate(['/meals']);
        this.meal = {
          id: 0,
          name: null,
          pricePerGr: 0,
        };
        this.mealForm = this._formBuilder.group({
          name: [
            '',
            [
              Validators.required,
              Validators.minLength(2),
              Validators.maxLength(60),
            ],
          ],
          pricePerGr: [
            '',
            [Validators.required, Validators.min(0), Validators.max(10000)],
          ],
        });
        this.mealForm.valueChanges.subscribe(() =>
          this.validateFormValue(this.mealForm)
        );

        this._activatedRoute.paramMap.subscribe(
          (params) => {
            const mealId = +params.get('id');
            if (mealId) {
              this.fillFormWithValuesToEdit(mealId);
            }
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

  validateFormValue(formGroup: FormGroup = this.mealForm): void {
    this.formErrors = this._errService.handleClientErrors(
      formGroup,
      this.messages
    );
  }

  fillFormWithValuesToEdit(id: number): void {
    this._mealService.getMeal(id).subscribe(
      (meal: IMeal) => {
        Object.assign(this.meal, meal);
        this.mealForm.patchValue({
          name: this.meal.name,
          pricePerGr: this.meal.pricePerGr,
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

  handleEditAction(meal: IMeal): void {
    this.mapFormValuesToModel();
    this._mealService.updateMeal(meal).subscribe(
      () => {
        this._router.navigate(['/meals']);
        Swal.fire({
          position: 'center',
          icon: 'success',
          title: 'Successfully edited!',
          showConfirmButton: false,
          timer: 1500,
        });
      },
      () => {
        this.mealForm.markAllAsTouched();
        this.validateFormValue();
        Swal.fire({
          position: 'center',
          icon: 'error',
          title: 'Oops...',
          text: 'Something went wrong!',
        });
      }
    );
  }

  handleCreateAction(): void {
    this.mapFormValuesToModel();
    this._mealService.createMeal(this.meal).subscribe(
      () => {
        this._router.navigate(['/meals']);
        Swal.fire({
          position: 'center',
          icon: 'success',
          title: 'Successfully created!',
          showConfirmButton: false,
          timer: 1500,
        });
      },
      () => {
        this.mealForm.markAllAsTouched();
        this.validateFormValue();
        Swal.fire({
          position: 'center',
          icon: 'error',
          title: 'Oops...',
          text: 'Something went wrong!',
        });
      }
    );
  }

  mapFormValuesToModel(): void {
    this.meal.name = this.mealForm.get('name').value;
    this.meal.pricePerGr = +this.mealForm.get('pricePerGr').value;
  }

  onSubmit(): void {
    if (this.meal.id != 0) {
      this.handleEditAction(this.meal);
    } else {
      this.handleCreateAction();
    }
  }
}
