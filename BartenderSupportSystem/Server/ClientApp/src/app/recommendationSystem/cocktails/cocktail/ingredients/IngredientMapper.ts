import { IDrink } from 'src/app/recommendationSystem/drinks/drink/IDrink';
import { IMeal } from 'src/app/recommendationSystem/meals/meal/IMeal';
import { ICocktail } from '../ICocktail';
import { IIngredient } from './IIngredient';

export class IngredientMapper {
    static toModelValues(cocktail: ICocktail, componentList: any[], ingredientsFormValues: any[]): IIngredient[] {
        let ingredients: IIngredient[] = new Array();
        ingredientsFormValues.forEach(e => {
            let componentId: number = +componentList.find(component => component.name === e.componentName).id;
            let component: any = componentList.find(component => component.id === e.componentId && component.name === e.componentName);
            let ingredient: IIngredient = {
                id: e.ingredientId,
                cocktailId: cocktail.id,
                componentId: componentId,
                meal: component ? <IMeal>component : null,
                drink: component ? <IDrink>component : null,
                proportionType: e.proportionType,
                proportionValue: +e.proportionValue
            }
            ingredients.push(ingredient);
        });
        return ingredients;
    }

    static toFormValues(ingredients: IIngredient[]): any[] {
        let ingredientsFormValues: any[] = new Array();
        ingredients.forEach((e: IIngredient) => {
            let ingredient: any = {
                ingredientId: e.id,
                componentName: e.drink ? e.drink.name : e.meal.name,
                proportionType: e.proportionType,
                proportionValue: e.proportionValue
            }
            ingredientsFormValues.push(ingredient);
        });
        return ingredientsFormValues;
    }
}