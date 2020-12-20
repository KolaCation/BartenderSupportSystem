import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { ErrorHandlerService } from 'src/app/shared/ErrorHandlerService';
import { CustomTestService } from '../custom-test/custom-test.service';

@Component({
  selector: 'app-custom-test-pass-form',
  templateUrl: './custom-test-pass-form.component.html',
  styleUrls: ['./custom-test-pass-form.component.css']
})
export class CustomTestPassFormComponent implements OnInit {

  constructor(private _formBuilder: FormBuilder, private _customTestService: CustomTestService,
    private _activatedRoute: ActivatedRoute, private _router: Router,
    private _errService: ErrorHandlerService, private _authorizeService: AuthorizeService) { }

  ngOnInit(): void {
    
  }

}
