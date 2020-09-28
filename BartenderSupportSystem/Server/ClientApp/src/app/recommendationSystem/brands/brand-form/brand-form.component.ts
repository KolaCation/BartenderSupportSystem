import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup, AbstractControl } from '@angular/forms';
import { IBrand } from '../brand/IBrand';
import { Countries } from '../brand/Countries';
import { CustomValidators } from '../../../shared/CustomValidators';
import { BrandService } from '../brand/brand.service';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-brand-form',
  templateUrl: './brand-form.component.html',
  styleUrls: ['./brand-form.component.css']
})
export class BrandFormComponent implements OnInit {

  brandForm: FormGroup;
  countryArray: Countries[] = Object.values(Countries);
  brand: IBrand;

  messages = {
    "name": {
      "minlength": "Name must be at least 2 chars long.",
      "maxlength": "Name must not exceed 60 chars.",
      "required": "Name is required."
    },
    "countryOfOrigin": {
      "required": "Country of origin is required.",
      "countryError": "Provide a country from the list."
    }
  }

  formErrors = {
    "name": "",
    "countryOfOrigin": ""
  }

  constructor(private _formBuilder: FormBuilder, private _brandService: BrandService,
    private _activatedRoute: ActivatedRoute, private _router: Router) { }

  ngOnInit(): void {
    this.brand = {
      id: 0,
      name: null,
      countryOfOrigin: null
    }
    this.brandForm = this._formBuilder.group({
      name: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(60)]],
      countryOfOrigin: ['', [Validators.required, CustomValidators.validateCountry()]]
    })
    this.brandForm.valueChanges.subscribe(() => this.validateFormValue(this.brandForm));

    this._activatedRoute.paramMap.subscribe(params => {
      const brandId = +params.get('id');
      if (brandId) {
        this.fillFormWithValuesToEdit(brandId);
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

  validateFormValue(formGroup: FormGroup = this.brandForm) {
    Object.keys(formGroup.controls).forEach((key: string) => {
      this.formErrors[key] = "";
      const abstractControl = this.brandForm.get(key);
      if (abstractControl && abstractControl.invalid && (abstractControl.touched || abstractControl.dirty)) {
        const messagesForControl = this.messages[key];
        for (let errorKey in abstractControl.errors) {
          this.formErrors[key] += messagesForControl[errorKey] + " ";
        }
      }

      if (abstractControl && abstractControl instanceof FormGroup) {
        this.validateFormValue(abstractControl);
      }
    });
  }

  fillFormWithValuesToEdit(id: number): void {
    this._brandService.getBrand(id).subscribe(
      (brand: IBrand) => {
        let countryName: string;
        Object.keys(Countries).forEach((country: Countries) => {
          if (brand.countryOfOrigin == country) {
            countryName = Countries[country];
          }
        });
        Object.assign(this.brand, brand);
        this.brandForm.patchValue({
          name: this.brand.name,
          countryOfOrigin: countryName
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

  handleEditAction(brand: IBrand): void {
    this.mapFormValuesToModel();
    this._brandService.updateBrand(brand).subscribe(
      () => {
        this._router.navigate(['/brands']);
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

  handleCreateAction(): void {
    this.mapFormValuesToModel();
    this._brandService.createBrand(this.brand).subscribe(
      () => {
        this._router.navigate(['/brands']);
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

  mapFormValuesToModel(): void {
    this.brand.name = this.brandForm.get('name').value;
    let countryCode: string = Object.keys(Countries).find(key => Countries[key] === this.brandForm.get('countryOfOrigin').value);
    this.brand.countryOfOrigin = countryCode;
  }


  onSubmit() {
    if (this.brand.id != 0) {
      this.handleEditAction(this.brand);
    }
    else {
      this.handleCreateAction();
    }
  }
}
