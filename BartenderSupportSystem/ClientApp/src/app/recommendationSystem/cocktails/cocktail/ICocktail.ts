import { IIngredient } from './ingredients/IIngredient';

export interface ICocktail {
  id: number;
  name: string;
  cocktailType: string;
  photoPath: string;
  description: string;
  ingredients?: IIngredient[];
}
