import { Countries } from '../recommendationSystem/brands/brand/Countries';
import { AlcoholType } from '../recommendationSystem/drinks/drink/AlcoholType';
import { AbstractControl } from '@angular/forms';

export class CustomValidators {
    static validateCountry() {
        return (control: AbstractControl): { [key: string]: any } | null => {
            if (control && (control.value === "" || Object.values(Countries).includes(control.value))) {
                return null;
            }
            else {
                return { "countryError": true };
            }
        }
    }

    static validateAlcoholType() {
        return (control: AbstractControl): { [key: string]: any } | null => {
            if (control && (control.value === "" || Object.values(AlcoholType).includes(control.value))) {
                return null;
            }
            else {
                return { "alcoholTypeError": true };
            }
        }
    }


    
    static validateBrand() {
        return (control: AbstractControl): { [key: string]: any } | null => {
            if (control && control.value != "") {
                return null;
            }
            else {
                return { "brandError": true };
            }
        }
    }
}