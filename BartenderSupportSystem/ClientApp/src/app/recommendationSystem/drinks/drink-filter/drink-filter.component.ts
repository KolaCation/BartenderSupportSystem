import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import Swal from 'sweetalert2';
import { BrandService } from '../../brands/brand/brand.service';
import { IBrand } from '../../brands/brand/IBrand';
import { AlcoholType } from '../drink/AlcoholType';
import { IDrinkFilter } from './IDrinkFilter';

@Component({
  selector: 'drink-filter',
  templateUrl: './drink-filter.component.html',
  styleUrls: ['./drink-filter.component.css'],
})
export class DrinkFilterComponent implements OnInit {
  drinkFilterForm: FormGroup;
  brands: IBrand[];
  drinkFilterModel: IDrinkFilter;
  @Output()
  filterDrinks: EventEmitter<IDrinkFilter> = new EventEmitter<IDrinkFilter>();
  alcoholTypesArray: AlcoholType[] = Object.values(AlcoholType);
  title = 'Show';
  isExpanded = false;

  constructor(
    private _formBuilder: FormBuilder,
    private _brandService: BrandService
  ) {}

  ngOnInit(): void {
    this._brandService.getBrands().subscribe(
      (data) => {
        this.brands = data;
        this.brands.sort((a, b) => {
          if (a.name < b.name) {
            return -1;
          }
          if (a.name > b.name) {
            return 1;
          }
          return 0;
        });
      },
      () => {
        Swal.fire({
          position: 'center',
          icon: 'error',
          title: 'Oops...',
          text: 'Something went wrong!',
        });
      }
    );
    this.drinkFilterModel = {
      name: null,
      alcoholType: null,
      alcoholPercentageMinValue: 0,
      alcoholPercentageMaxValue: 0,
      flavor: null,
      brandId: 0,
      brand: null,
      pricePerMl: null,
      isNonAlcohol: false,
    };
    this.drinkFilterForm = this._formBuilder.group({
      name: [''],
      alcoholType: [''],
      alcoholPercentageMinValue: [''],
      alcoholPercentageMaxValue: [''],
      flavor: [''],
      pricePerMl: [''],
      brandId: [''],
      isNonAlcohol: [''],
    });

    this.drinkFilterForm.valueChanges.subscribe(
      () => this.onSubmit(),
      () => {
        Swal.fire({
          position: 'center',
          icon: 'error',
          title: 'Oops...',
          text: 'Something went wrong!',
        });
      }
    );
  }

  mapFormValuesToModel(): void {
    this.drinkFilterModel.name = this.drinkFilterForm.get('name').value;
    this.drinkFilterModel.alcoholType = this.drinkFilterForm.get(
      'alcoholType'
    ).value;
    this.drinkFilterModel.alcoholPercentageMinValue = +this.drinkFilterForm.get(
      'alcoholPercentageMinValue'
    ).value;
    this.drinkFilterModel.alcoholPercentageMaxValue = +this.drinkFilterForm.get(
      'alcoholPercentageMaxValue'
    ).value;
    this.drinkFilterModel.flavor = this.drinkFilterForm.get('flavor').value;
    this.drinkFilterModel.brandId = +this.drinkFilterForm.get('brandId').value;
    this.drinkFilterModel.pricePerMl = this.drinkFilterForm.get(
      'pricePerMl'
    ).value;
    this.drinkFilterModel.isNonAlcohol = this.drinkFilterForm.get(
      'isNonAlcohol'
    ).value;
  }

  onSubmit(): void {
    this.mapFormValuesToModel();
    this.filterDrinks.emit(this.drinkFilterModel);
  }

  clearForm(): void {
    this.drinkFilterForm.setValue({
      name: '',
      alcoholType: '',
      alcoholPercentageMinValue: '',
      alcoholPercentageMaxValue: '',
      flavor: '',
      pricePerMl: '',
      brandId: '',
      isNonAlcohol: '',
    });
    this.filterDrinks.emit(null);
  }

  toggleContent(): void {
    this.isExpanded = !this.isExpanded;
    if (this.isExpanded) {
      this.title = 'Hide';
    } else {
      this.title = 'Show';
    }
  }
}
