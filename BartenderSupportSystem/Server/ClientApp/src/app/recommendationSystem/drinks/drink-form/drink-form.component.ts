import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomValidators } from 'src/app/shared/CustomValidators';
import { ErrorHandlerService } from 'src/app/shared/ErrorHandlerService';
import Swal from 'sweetalert2';
import { BrandService } from '../../brands/brand/brand.service';
import { IBrand } from '../../brands/brand/IBrand';
import { AlcoholType } from '../drink/AlcoholType';
import { DrinkService } from '../drink/drink.service';
import { IDrink } from '../drink/IDrink';

@Component({
  selector: 'app-drink-form',
  templateUrl: './drink-form.component.html',
  styleUrls: ['./drink-form.component.css']
})
export class DrinkFormComponent implements OnInit {

  drinkForm: FormGroup;
  brands: IBrand[];
  drink: IDrink;
  alcoholTypesArray: AlcoholType[] = Object.values(AlcoholType);
  private _photoPath: string;

  messages = {
    "name": {
      "minlength": "Name must be at least 2 chars long.",
      "maxlength": "Name must not exceed 60 chars.",
      "required": "Name is required."
    },
    "alcoholType": {
      "required": "Alcohol type is required.",
      "alcoholTypeError": "Provide a type from the list."
    },
    "alcoholPercentage": {
      "required": "Alcohol percentage is required.",
      "min": "Min value: 0.",
      "max": "Max value: 100."
    },
    "flavor": {
      "required": "Flavor is required.",
      "minlength": "Flavor must be at least 2 chars long.",
      "maxlength": "Flavor must not exceed 255 chars.",
    },
    "pricePerMl": {
      "required": "Price per ml is required.",
      "min": "Min value: 0.",
      "max": "Max value: 10000."
    },
    "brandId": {
      "brandError": "Select a brand from the list."
    }
  }

  formErrors = {
    "name": "",
    "alcoholType": "",
    "alcoholPercentage": "",
    "flavor": "",
    "pricePerMl": "",
    "brandId": ""
  }

  constructor(private _formBuilder: FormBuilder, private _drinkService: DrinkService,
    private _activatedRoute: ActivatedRoute, private _router: Router, private _brandService: BrandService,
    private _errService: ErrorHandlerService) { }

  ngOnInit(): void {
    this._brandService.getBrands().subscribe(
      data => {
        this.brands = data;
        this.brands.sort((a, b) => {
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
    this.drink = {
      id: 0,
      name: null,
      alcoholType: null,
      alcoholPercentage: 0,
      flavor: null,
      brandId: 0,
      brand: null,
      pricePerMl: 0,
      photoPath: null,
    }
    this.drinkForm = this._formBuilder.group({
      name: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(60)]],
      alcoholType: ['', [Validators.required, CustomValidators.validateAlcoholType()]],
      alcoholPercentage: ['', [Validators.required, Validators.min(0), Validators.max(100)]],
      flavor: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(255)]],
      pricePerMl: ['', [Validators.required, Validators.min(0), Validators.max(10000)]],
      photoPath: [''],
      brandId: ['', CustomValidators.validateBrand()]
    });

    this.drinkForm.valueChanges.subscribe(() => {
      this.validateFormValue(this.drinkForm);
    });

    this._activatedRoute.paramMap.subscribe(params => {
      const drinkId = +params.get('id');
      if (drinkId) {
        this.fillFormWithValuesToEdit(drinkId);
      }
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

  validateFormValue(formGroup: FormGroup = this.drinkForm) {
    this.formErrors = this._errService.handleClientErrors(formGroup, this.messages);
  }

  fillFormWithValuesToEdit(id: number): void {
    this._drinkService.getDrink(id).subscribe(
      (drink: IDrink) => {
        Object.assign(this.drink, drink);
        this.drinkForm.patchValue({
          name: this.drink.name,
          alcoholType: this.drink.alcoholType,
          alcoholPercentage: this.drink.alcoholPercentage,
          flavor: this.drink.flavor,
          brandId: this.drink.brandId,
          brand: this.drink.brand,
          pricePerMl: this.drink.pricePerMl,
          photoPath: this.drink.photoPath
        });
        this._photoPath = this.drink.photoPath;
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

  handleEditAction(drink: IDrink): void {
    this.mapFormValuesToModel();
    if (this._photoPath == this.drink.photoPath) {
      this.drink.photoPath = null;
    }
    this._drinkService.updateDrink(drink).subscribe(
      () => {
        this._router.navigate(['/drinks']);
        Swal.fire({
          position: 'center',
          icon: 'success',
          title: 'Successfully edited!',
          showConfirmButton: false,
          timer: 1500
        })
      },
      error => {
        this.formErrors = this._errService.handleServerErrors(this.drinkForm, error.errors);
        Swal.fire({
          position: 'center',
          icon: 'error',
          title: 'Oops...',
          text: 'Something went wrong!'
        });
      }
    );
  }

  handleCreateAction(): void {
    this.mapFormValuesToModel();
    this._drinkService.createDrink(this.drink).subscribe(
      () => {
        this._router.navigate(['/drinks']);
        Swal.fire({
          position: 'center',
          icon: 'success',
          title: 'Successfully created!',
          showConfirmButton: false,
          timer: 1500
        });
      },
      error => {
        this.formErrors = this._errService.handleServerErrors(this.drinkForm, error.errors);
        Swal.fire({
          position: 'center',
          icon: 'error',
          title: 'Oops...',
          text: 'Something went wrong!'
        });
      }
    );
  }

  mapFormValuesToModel(): void {
    this.drink.name = this.drinkForm.get('name').value;
    this.drink.alcoholType = this.drinkForm.get('alcoholType').value;
    this.drink.alcoholPercentage = +this.drinkForm.get('alcoholPercentage').value;
    this.drink.flavor = this.drinkForm.get('flavor').value;
    this.drink.brandId = +this.drinkForm.get('brandId').value;
    this.drink.pricePerMl = +this.drinkForm.get('pricePerMl').value;
  }


  onSubmit() {
    if (this.drink.id != 0) {
      this.handleEditAction(this.drink);
    }
    else {
      this.handleCreateAction();
    }
  }

  fillModelWithPictureUrl(result: string) {
    this.drinkForm.patchValue({ photoPath: result });
    this.drink.photoPath = result;
  }
}