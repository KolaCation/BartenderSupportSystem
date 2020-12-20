import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { ErrorHandlerService } from 'src/app/shared/ErrorHandlerService';
import { CustomTestService } from '../custom-test/custom-test.service';
import { ICustomTest } from '../custom-test/ICustomTest';

@Component({
  selector: 'app-custom-test-list',
  templateUrl: './custom-test-list.component.html',
  styleUrls: ['./custom-test-list.component.css']
})
export class CustomTestListComponent implements OnInit {

  tests: ICustomTest[];
  statusMessage: string = "Loading...";

  constructor(private _formBuilder: FormBuilder, private _customTestService: CustomTestService,
    private _activatedRoute: ActivatedRoute, private _router: Router,
    private _errService: ErrorHandlerService, private _authorizeService: AuthorizeService) { }

 
    ngOnInit(): void {
      this._customTestService.getCustomTests().subscribe(
        data => {
          if (data.length === 0) {
            this.statusMessage = "No tests to display.";
          } else {
            this.tests = data; 
          }
        },
        error => {
          this.statusMessage = error;
        }
      );
    }

}
