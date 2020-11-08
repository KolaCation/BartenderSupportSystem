import { Countries } from '../recommendationSystem/brands/brand/Countries';
import { AlcoholType } from '../recommendationSystem/drinks/drink/AlcoholType';
import { AbstractControl } from '@angular/forms';
import { MealType } from '../recommendationSystem/meals/meal/MealType';
import { CocktailType } from '../recommendationSystem/cocktails/cocktail/CocktailType';
import { ProportionType } from '../recommendationSystem/cocktails/cocktail/ingredients/ProportionType';

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

    static validateMealType() {
        return (control: AbstractControl): { [key: string]: any } | null => {
            if (control && (control.value !== "" || Object.values(MealType).includes(control.value))) {
                return null;
            }
            else {
                return { "mealTypeError": true };
            }
        }
    }

    static validateCocktailType() {
        return (control: AbstractControl): { [key: string]: any } | null => {
            if (control && (control.value !== "" || Object.values(CocktailType).includes(control.value))) {
                return null;
            }
            else {
                return { "cocktailTypeError": true };
            }
        }
    }

    static validateProportionType() {
        return (control: AbstractControl): { [key: string]: any } | null => {
            if (control && (control.value !== "" || Object.values(ProportionType).includes(control.value))) {
                return null;
            }
            else {
                return { "proportionTypeError": true };
            }
        }
    }

    static componentExists(componentList: any[]) {
        return (control: AbstractControl): { [key: string]: any } | null => {
            if (control && componentList && (control.value === "" || componentList.find(e=>e.name===control.value))) {
                return null;
            }
            else {
                return { "componentError": true };
            }
        }
    }
}