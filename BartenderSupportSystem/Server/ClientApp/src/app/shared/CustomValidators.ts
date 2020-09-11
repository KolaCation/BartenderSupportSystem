import { Countries } from '../recommendationSystem/brands/brand/Countries';
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
}