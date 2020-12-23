import { IUserRating } from './IUserRating';

export interface IRating {
  id: number;
  testId: number;
  mark: number;
  quantityOfRaters: number;
  ratingList: IUserRating[];
}
