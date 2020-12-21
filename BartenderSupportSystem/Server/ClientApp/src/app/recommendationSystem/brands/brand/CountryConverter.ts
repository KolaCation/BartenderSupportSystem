import { Countries } from './Countries';

export class CountryConverter {
  static fromCodeToName(countryCode: string): string {
    let countryName: string;
    Object.keys(Countries).forEach((country: Countries) => {
      if (countryCode == country) {
        countryName = Countries[country];
      }
    });
    return countryName;
  }

  static fromNameToCode(countryName: string): string {
    const countryCode: string = Object.keys(Countries).find(
      (key) => Countries[key] === countryName
    );
    return countryCode;
  }
}
