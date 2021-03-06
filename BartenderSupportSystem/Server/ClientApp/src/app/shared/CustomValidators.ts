import { Countries } from '../recommendationSystem/brands/brand/Countries';
import { AlcoholType } from '../recommendationSystem/drinks/drink/AlcoholType';
import { AbstractControl, FormArray, FormGroup } from '@angular/forms';
import { CocktailType } from '../recommendationSystem/cocktails/cocktail/CocktailType';
import { ProportionType } from '../recommendationSystem/cocktails/cocktail/ingredients/ProportionType';

export class CustomValidators {
  static validateCountry() {
    return (control: AbstractControl): { [key: string]: any } | null => {
      if (
        control &&
        (control.value === '' ||
          Object.values(Countries).includes(control.value))
      ) {
        return null;
      } else {
        return { countryError: true };
      }
    };
  }

  static validateAlcoholType() {
    return (control: AbstractControl): { [key: string]: any } | null => {
      if (
        control &&
        (control.value === '' ||
          Object.values(AlcoholType).includes(control.value))
      ) {
        return null;
      } else {
        return { alcoholTypeError: true };
      }
    };
  }

  static validateBrand() {
    return (control: AbstractControl): { [key: string]: any } | null => {
      if (control && control.value != '') {
        return null;
      } else {
        return { brandError: true };
      }
    };
  }

  static validateCocktailType() {
    return (control: AbstractControl): { [key: string]: any } | null => {
      if (
        control &&
        (control.value !== '' ||
          Object.values(CocktailType).includes(control.value))
      ) {
        return null;
      } else {
        return { cocktailTypeError: true };
      }
    };
  }

  static validateProportionType() {
    return (control: AbstractControl): { [key: string]: any } | null => {
      if (
        control &&
        (control.value !== '' ||
          Object.values(ProportionType).includes(control.value))
      ) {
        return null;
      } else {
        return { proportionTypeError: true };
      }
    };
  }

  static componentExists(componentList: any[]) {
    return (control: AbstractControl): { [key: string]: any } | null => {
      if (
        control &&
        componentList &&
        (control.value === '' ||
          componentList.find((e) => e.name === control.value))
      ) {
        return null;
      } else {
        return { componentError: true };
      }
    };
  }

  static validateArrayMinLength(min: number) {
    return (control: AbstractControl): { [key: string]: any } | null => {
      if (
        control &&
        control instanceof FormArray &&
        (<FormArray>control).length >= min
      ) {
        return null;
      } else {
        return { arrayminlength: true };
      }
    };
  }

  static validateArrayMaxLength(max: number) {
    return (control: AbstractControl): { [key: string]: any } | null => {
      if (
        control &&
        control instanceof FormArray &&
        (<FormArray>control).length <= max
      ) {
        return null;
      } else {
        return { arraymaxlength: true };
      }
    };
  }

  static atLeastOneCorrectAnswerExists() {
    return (control: AbstractControl): { [key: string]: any } | null => {
      if (control && control instanceof FormArray) {
        const checkList: boolean[] = [];
        (<FormArray>control).controls.forEach((control) => {
          if (control instanceof FormGroup) {
            checkList.push((<FormGroup>control).get('answerIsCorrect').value);
          }
        });
        if (checkList.includes(true)) {
          return null;
        } else {
          return { correctAnswersCount: true };
        }
      }
    };
  }
}
