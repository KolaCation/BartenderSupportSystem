import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup, AbstractControl } from '@angular/forms';
import { IBrand } from '../brand/IBrand';
import { Countries } from '../brand/Countries';
import { CustomValidators } from '../../../shared/CustomValidators';
import { BrandService } from '../brand/brand.service';

@Component({
  selector: 'app-brand-form',
  templateUrl: './brand-form.component.html',
  styleUrls: ['./brand-form.component.css']
})
export class BrandFormComponent implements OnInit {

  brandForm: FormGroup;
  countryArray: Countries[] = Object.values(Countries);

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

  constructor(private _formBuilder: FormBuilder, private _brandService: BrandService) { }

  ngOnInit(): void {
    this.brandForm = this._formBuilder.group({
      name: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(60)]],
      countryOfOrigin: ['', [Validators.required, CustomValidators.validateCountry()]]
    })
    this.brandForm.valueChanges.subscribe(() => this.validateFormValue(this.brandForm));
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
    })
  }

  onSubmit() {
    console.log(this.brandForm.get('countryOfOrigin').value);
    console.log(typeof this.brandForm.get('countryOfOrigin').value);
    let countryCode: string = Object.keys(Countries).find(key => Countries[key] === this.brandForm.get('countryOfOrigin').value);
    console.log(countryCode);
    let brand: IBrand = {
      id: 0,
      name: this.brandForm.get('name').value,
      countryOfOrigin: countryCode
    }
    this._brandService.createBrand(brand).subscribe(
      response => console.log(response),
      error => console.log(error)
    );
  }
}