import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomValidators } from 'src/app/shared/CustomValidators';
import { ErrorHandlerService } from 'src/app/shared/ErrorHandlerService';
import Swal from 'sweetalert2';
import { DrinkService } from '../../drinks/drink/drink.service';
import { IDrink } from '../../drinks/drink/IDrink';
import { IMeal } from '../../meals/meal/IMeal';
import { MealService } from '../../meals/meal/meal.service';
import { CocktailService } from '../cocktail/cocktail.service';
import { CocktailType } from '../cocktail/CocktailType';
import { ICocktail } from '../cocktail/ICocktail';
import { ProportionType } from '../cocktail/ingredients/ProportionType';
import { IngredientMapper } from '../cocktail/ingredients/IngredientMapper';
import { IIngredient } from '../cocktail/ingredients/IIngredient';

@Component({
  selector: 'app-cocktail-form',
  templateUrl: './cocktail-form.component.html',
  styleUrls: ['./cocktail-form.component.css']
})
export class CocktailFormComponent implements OnInit {

  cocktailForm: FormGroup;
  drinks: IDrink[];
  meals: IMeal[];
  cocktail: ICocktail;
  private _photoPath: string;
  proportionTypeArray = Object.values(ProportionType);
  cocktailTypeArray = Object.values(CocktailType);
  componentList: any[] = new Array();

  messages = {
    "name": {
      "minlength": "Name must be at least 2 chars long.",
      "maxlength": "Name must not exceed 60 chars.",
      "required": "Name is required."
    },
    "cocktailType": {
      "required": "Cocktail type is required.",
      "cocktailTypeError": "Provide a type from the list."
    },
    "description": {
      "required": "Description is required.",
      "minlength": "Description must be at least 2 chars long.",
      "maxlength": "Description must not exceed 255 chars.",
    },
  }

  formErrors = {
    "name": "",
    "cocktailType": "",
    "description": ""
  }

  constructor(private _formBuilder: FormBuilder, private _cocktailService: CocktailService,
    private _activatedRoute: ActivatedRoute, private _router: Router,
    private _errService: ErrorHandlerService, private _drinkService: DrinkService,
    private _mealService: MealService) { }

  ngOnInit(): void {
    this._drinkService.getDrinks().subscribe(
      data => {
        this.drinks = data;
        this.drinks.forEach(el => this.componentList.push(el));
        this.drinks.sort((a, b) => {
          if (a.name < b.name) {
            return -1;
          }
          if (a.name > b.name) {
            return 1
          }
          return 0;
        });
      },
      error => {
        console.log(error);
        Swal.fire({
          position: 'center',
          icon: 'error',
          title: 'Oops...',
          text: 'Something went wrong!'
        });
      }
    );
    this._mealService.getMeals().subscribe(
      data => {
        this.meals = data;
        this.meals.forEach(el => this.componentList.push(el));
        this.meals.sort((a, b) => {
          if (a.name < b.name) {
            return -1;
          }
          if (a.name > b.name) {
            return 1
          }
          return 0;
        });
      },
      error => {
        console.log(error);
        Swal.fire({
          position: 'center',
          icon: 'error',
          title: 'Oops...',
          text: 'Something went wrong!'
        });
      }
    );
    this.cocktail = {
      id: 0,
      name: null,
      cocktailType: null,
      photoPath: null,
      ingredients: null,
      description: null
    }
    this.cocktailForm = this._formBuilder.group({
      name: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(60)]],
      cocktailType: ['', [Validators.required, CustomValidators.validateCocktailType()]],
      description: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(255)]],
      ingredients: this._formBuilder.array([
        this.addIngredientFormGroup()
      ])
    });

    this.cocktailForm.valueChanges.subscribe(() => {
      this.validateFormValue(this.cocktailForm);
    });

    this._activatedRoute.paramMap.subscribe(params => {
      const cocktailId = +params.get('id');
      if (cocktailId) {
        this.fillFormWithValuesToEdit(cocktailId);
      }
    },
      error => {
        Swal.fire({
          position: 'center',
          icon: 'error',
          title: 'Oops...',
          text: 'Something went wrong!'
        });
      }
    );
    //ngOnInit end
  }

  validateFormValue(formGroup: FormGroup = this.cocktailForm) {
    this.formErrors = this._errService.handleClientErrors(formGroup, this.messages);
  }

  addIngredientClick(): void {
    (<FormArray>this.cocktailForm.get('ingredients')).push(this.addIngredientFormGroup());
  }

  addIngredientFormGroup(): FormGroup {
    return this._formBuilder.group({
      ingredientId: [0, [Validators.required]],
      componentName: ['', [Validators.required, CustomValidators.componentExists(this.componentList)]],
      proportionType: ['', [Validators.required, CustomValidators.validateProportionType()]],
      proportionValue: ['', [Validators.required, Validators.min(0), Validators.max(10000)]]
    });
  }

  fillFormWithValuesToEdit(id: number): void {
    this._cocktailService.getCocktail(id).subscribe(
      (cocktail: ICocktail) => {
        Object.assign(this.cocktail, cocktail);
        this.cocktailForm.patchValue({
          name: this.cocktail.name,
          cocktailType: this.cocktail.cocktailType,
          description: this.cocktail.description,
          photoPath: this.cocktail.photoPath
        });
        this.cocktailForm.setControl('ingredients', this.generateFormArray(this.cocktail.ingredients));
        this._photoPath = this.cocktail.photoPath;
      },
      error => {
        Swal.fire({
          position: 'center',
          icon: 'error',
          title: 'Oops...',
          text: 'Something went wrong!'
        });
      }
    );
  }

  generateFormArray(ingredients: IIngredient[]): FormArray {
    let formArr: FormArray = new FormArray([]);
    let ingredientFormValues = IngredientMapper.toFormValues(ingredients);
    ingredientFormValues.forEach((ingredient: any) => {
      let formGroup: FormGroup = this._formBuilder.group({
        ingredientId: [ingredient.ingredientId, [Validators.required]],
        componentName: [ingredient.componentName, [Validators.required, CustomValidators.componentExists(this.componentList)]],
        proportionType: [ingredient.proportionType, [Validators.required, CustomValidators.validateProportionType()]],
        proportionValue: [ingredient.proportionValue, [Validators.required, Validators.min(0), Validators.max(10000)]]
      });
      formArr.push(formGroup);
    });
    return formArr;
  }

  mapFormValuesToModel(): void {
    this.cocktail.name = this.cocktailForm.get('name').value;
    this.cocktail.cocktailType = this.cocktailForm.get('cocktailType').value;
    this.cocktail.description = this.cocktailForm.get('description').value;
    this.cocktail.ingredients = IngredientMapper.toModelValues(this.cocktail, this.componentList, this.cocktailForm.get('ingredients').value);
  }

  handleCreateAction(): void {
    this.mapFormValuesToModel();
    this._cocktailService.createCocktail(this.cocktail).subscribe(
      () => {
        this._router.navigate(['/cocktails']);
        Swal.fire({
          position: 'center',
          icon: 'success',
          title: 'Successfully created!',
          showConfirmButton: false,
          timer: 1500
        });
      },
      error => {
        console.log(error);
        Swal.fire({
          position: 'center',
          icon: 'error',
          title: 'Oops...',
          text: 'Something went wrong!'
        });
      }
    );
  }

  handleEditAction(cocktail: ICocktail): void {
    this.mapFormValuesToModel();
    if (this._photoPath == this.cocktail.photoPath) {
      this.cocktail.photoPath = null;
    }
    this._cocktailService.updateCocktail(cocktail).subscribe(
      () => {
        this._router.navigate(['/cocktails']);
        Swal.fire({
          position: 'center',
          icon: 'success',
          title: 'Successfully edited!',
          showConfirmButton: false,
          timer: 1500
        })
      },
      error => {
        console.log(error);
        Swal.fire({
          position: 'center',
          icon: 'error',
          title: 'Oops...',
          text: 'Something went wrong!'
        });
      }
    );
  }

  onSubmit() {
    if (this.cocktail.id != 0) {
      this.handleEditAction(this.cocktail);
    }
    else {
      this.handleCreateAction();
    }
  }

  RemoveIngredient(i: number): void {
    (<FormArray>this.cocktailForm.get('ingredients')).removeAt(i);
  }

  fillModelWithPictureUrl(result: string) {
    this.cocktailForm.patchValue({ photoPath: result });
    this.cocktail.photoPath = result;
  }
}
