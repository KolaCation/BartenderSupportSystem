import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup, AbstractControl } from '@angular/forms';
import { IBrand } from '../brand/IBrand';
import { Countries } from '../brand/Countries';
import { CustomValidators } from '../../../shared/CustomValidators';

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

  constructor(private _formBuilder: FormBuilder) { }

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
    console.log(Object.keys(Countries).find(key => key === this.brandForm.controls.countryOfOrigin.value))
    console.log(this.brandForm);
  }
}