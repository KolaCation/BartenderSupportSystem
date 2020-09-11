import { IBrand } from '../../brands/brand/IBrand';
export interface IDrink {
    id: number,
    name: string,
    alcoholType: string,
    alcoholPercentage: number,
    flavor: string,
    brandId: number,
    brand?: IBrand,
    pricePerMl: number,
    photoPath: string
}