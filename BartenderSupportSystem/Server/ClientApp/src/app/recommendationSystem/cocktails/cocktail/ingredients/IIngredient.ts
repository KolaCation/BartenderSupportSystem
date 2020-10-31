import { IMeal } from '../../../meals/meal/IMeal';
import { IDrink } from '../../../drinks/drink/IDrink';

export interface IIngredient {
    id: number,
    componentId: number,
    cocktailId: number,
    meal?: IMeal,
    drink?: IDrink,
    proportionType: string,
    proportionValue: number
}