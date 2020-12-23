import { Component, OnInit } from '@angular/core';
import { BrandService } from '../brand/brand.service';
import { Router } from '@angular/router';
import { IBrand } from '../brand/IBrand';
import { Countries } from '../brand/Countries';
import Swal from 'sweetalert2';
import { AuthorizeService } from 'src/api-authorization/authorize.service';

@Component({
  selector: 'app-brand-list',
  templateUrl: './brand-list.component.html',
  styleUrls: ['./brand-list.component.css'],
})
export class BrandListComponent implements OnInit {
  brands: IBrand[];
  statusMessage = 'Loading...';
  isAdmin = false;

  constructor(
    private _brandService: BrandService,
    private _router: Router,
    private _authorizeService: AuthorizeService
  ) {}

  ngOnInit(): void {
    this._authorizeService.getUserRole().subscribe(
      (role) => {
        this.isAdmin = role.toLowerCase() === 'admin';
        this._brandService.getBrands().subscribe(
          (data) => {
            if (data.length === 0) {
              this.statusMessage = 'No brands to display.';
            } else {
              this.brands = data;
              this.brands.forEach((brand: IBrand) => {
                let countryName: string;
                Object.keys(Countries).forEach((country: Countries) => {
                  if (brand.countryOfOrigin == country) {
                    countryName = Countries[country];
                  }
                });
                brand.countryOfOrigin = countryName;
              });
            }
          },
          (error) => {
            this.statusMessage = error;
          }
        );
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
  }

  editBrand(id: number): void {
    this._router.navigate(['/brands/edit', id]);
  }

  deleteBrand(brand: IBrand): void {
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!',
    }).then((result: { isConfirmed: boolean }) => {
      if (result.isConfirmed) {
        this._brandService.deleteBrand(brand.id).subscribe(
          () => {
            const brandIndex: number = this.brands.indexOf(brand, 0);
            this.brands.splice(brandIndex, 1);
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
        Swal.fire('Deleted!', 'Your record has been deleted.', 'success');
      }
    });
  }
}
