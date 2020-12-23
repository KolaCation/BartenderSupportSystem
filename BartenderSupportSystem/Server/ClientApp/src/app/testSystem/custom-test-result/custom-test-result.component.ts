import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import Swal from 'sweetalert2';
import { CustomTestService } from '../custom-test/custom-test.service';
import { ICustomTest } from '../custom-test/ICustomTest';
import { CustomTestResultService } from './custom-test-result.service';
import { ICustomTestResult } from './ICustomTestResult';

@Component({
  selector: 'app-custom-test-result',
  templateUrl: './custom-test-result.component.html',
  styleUrls: ['./custom-test-result.component.css'],
})
export class CustomTestResultComponent implements OnInit {
  origin: ICustomTest;
  result: ICustomTestResult;
  currentUserIsPasser = false;
  currentUserIsCreator = false;

  constructor(
    private _customTestService: CustomTestService,
    private _activatedRoute: ActivatedRoute,
    private _router: Router,
    private _authorizeService: AuthorizeService,
    private _customTestResultService: CustomTestResultService
  ) {}

  ngOnInit(): void {
    this.origin = {
      id: 0,
      name: null,
      topic: null,
      description: null,
      questions: null,
      authorUsername: null,
    };
    this.result = {
      id: 0,
      customTestId: 0,
      personalMark: 0,
      userName: null,
      pickedAnswers: null,
    };
    this._activatedRoute.paramMap.subscribe(
      (params) => {
        const testId = +params.get('id');
        this._authorizeService.getUserName().subscribe(
          (name) => {
            this._customTestResultService
              .getUserCustomTestResults(name, testId)
              .subscribe(
                (customTestResults: ICustomTestResult[]) => {
                  if (
                    customTestResults[0].userName.toLowerCase() ===
                    name.toLowerCase()
                  ) {
                    this.currentUserIsPasser = true;
                    this.result = customTestResults[0];
                    this._customTestService.getCustomTest(testId).subscribe(
                      (test: ICustomTest) => {
                        this.origin = test;
                        if (
                          test.authorUsername.toLowerCase() ===
                          name.toLowerCase()
                        ) {
                          this.currentUserIsCreator = true;
                        } else {
                          this.currentUserIsCreator = false;
                        }
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
                  } else {
                    this.currentUserIsPasser = false;
                    this._router.navigate(['/tests']);
                  }
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

  deleteTest(): void {
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
        this._customTestService.deleteCustomTest(this.origin.id).subscribe(
          () => this._router.navigate(['/tests']),
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
