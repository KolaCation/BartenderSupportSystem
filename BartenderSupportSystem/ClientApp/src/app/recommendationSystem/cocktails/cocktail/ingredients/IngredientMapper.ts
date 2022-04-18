import { IDrink } from 'src/app/recommendationSystem/drinks/drink/IDrink';
import { IMeal } from 'src/app/recommendationSystem/meals/meal/IMeal';
import { ICocktail } from '../ICocktail';
import { IIngredient } from './IIngredient';

export class IngredientMapper {
  static toModelValues(
    cocktail: ICocktail,
    componentList: any[],
    ingredientsFormValues: any[]
  ): IIngredient[] {
    if (
      cocktail &&
      componentList &&
      ingredientsFormValues &&
      ingredientsFormValues.length > 0
    ) {
      const ingredients: IIngredient[] = [];
      ingredientsFormValues.forEach((e) => {
        const findComponentByName: any = componentList.find(
          (component) => component.name === e.componentName
        );
        const componentId: number = findComponentByName
          ? +findComponentByName.id
          : 0;
        const component: any = componentList.find(
          (component) =>
            component.id === e.componentId && component.name === e.componentName
        );
        const ingredient: IIngredient = {
          id: e.ingredientId,
          cocktailId: cocktail.id,
          componentId: componentId,
          meal: component ? <IMeal>component : null,
          drink: component ? <IDrink>component : null,
          proportionType: e.proportionType,
          proportionValue: +e.proportionValue,
        };
        ingredients.push(ingredient);
      });
      return ingredients;
    } else {
      return null;
    }
  }

  static toFormValues(ingredients: IIngredient[]): any[] {
    if (ingredients) {
      const ingredientsFormValues: any[] = [];
      ingredients.forEach((e: IIngredient) => {
        const ingredient: any = {
          ingredientId: e.id,
          componentName: e.drink ? e.drink.name : e.meal.name,
          proportionType: e.proportionType,
          proportionValue: e.proportionValue,
        };
        ingredientsFormValues.push(ingredient);
      });
      return ingredientsFormValues;
    } else {
      return null;
    }
  }
}
