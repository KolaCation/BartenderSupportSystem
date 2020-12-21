import { IBrand } from '../../brands/brand/IBrand';
export interface IDrinkFilter {
  name: string;
  alcoholType: string;
  alcoholPercentageMinValue: number;
  alcoholPercentageMaxValue: number;
  isNonAlcohol: boolean;
  flavor: string;
  brandId: number;
  brand?: IBrand;
  pricePerMl: string;
}
