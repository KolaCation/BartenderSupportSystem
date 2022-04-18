import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import Swal from 'sweetalert2';
import { CocktailType } from '../cocktail/CocktailType';
import { ICocktailFilter } from './ICocktailFilter';

@Component({
  selector: 'cocktail-filter',
  templateUrl: './cocktail-filter.component.html',
  styleUrls: ['./cocktail-filter.component.css'],
})
export class CocktailFilterComponent implements OnInit {
  cocktailFilterForm: FormGroup;
  cocktailFilterModel: ICocktailFilter;
  @Output()
  filterCocktails: EventEmitter<ICocktailFilter> = new EventEmitter<ICocktailFilter>();
  cocktailTypeArray = Object.values(CocktailType);

  constructor(private _formBuilder: FormBuilder) {}

  ngOnInit(): void {
    this.cocktailFilterModel = {
      name: null,
      cocktailType: null,
      description: null,
    };
    this.cocktailFilterForm = this._formBuilder.group({
      name: [''],
      cocktailType: [''],
      description: [''],
    });

    this.cocktailFilterForm.valueChanges.subscribe(
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
    this.cocktailFilterModel.name = this.cocktailFilterForm.get('name').value;
    this.cocktailFilterModel.cocktailType = this.cocktailFilterForm.get(
      'cocktailType'
    ).value;
    this.cocktailFilterModel.description = this.cocktailFilterForm.get(
      'description'
    ).value;
  }

  onSubmit(): void {
    this.mapFormValuesToModel();
    this.filterCocktails.emit(this.cocktailFilterModel);
  }

  clearForm(): void {
    this.cocktailFilterForm.setValue({
      name: '',
      cocktailType: '',
      description: '',
    });
    this.filterCocktails.emit(null);
  }
}
