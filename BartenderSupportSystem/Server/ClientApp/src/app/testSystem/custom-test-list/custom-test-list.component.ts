import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import Swal from 'sweetalert2';
import { CustomTestResultService } from '../custom-test-result/custom-test-result.service';
import { ICustomTestResult } from '../custom-test-result/ICustomTestResult';
import { CustomTestService } from '../custom-test/custom-test.service';
import { ICustomTest } from '../custom-test/ICustomTest';
import { ICurrentUserIsCreator } from './ICurrentUserIsCreator';

@Component({
  selector: 'app-custom-test-list',
  templateUrl: './custom-test-list.component.html',
  styleUrls: ['./custom-test-list.component.css'],
})
export class CustomTestListComponent implements OnInit {
  tests: ICustomTest[] = [];
  statusMessage = 'Loading...';
  currentUserIsCreatorList: ICurrentUserIsCreator[] = [];

  constructor(
    private _customTestService: CustomTestService,
    private _router: Router,
    private _authorizeService: AuthorizeService,
    private _customTestResultService: CustomTestResultService
  ) {}

  ngOnInit(): void {
    this._customTestService.getCustomTests().subscribe(
      (tests: ICustomTest[]) => {
        if (tests.length === 0) {
          this.statusMessage = 'No tests to display.';
        }
        this._authorizeService.getUserName().subscribe(
          (name) => {
            this._customTestResultService
              .getUserCustomTestResults(name)
              .subscribe(
                (results: ICustomTestResult[]) => {
                  if (tests.length !== 0) {
                    tests.forEach((test: ICustomTest) => {
                      if (
                        results.find((e) => e.customTestId === test.id) == null
                      ) {
                        this.tests.push(test);
                      }
                      if (
                        test.authorUsername.toLowerCase() === name.toLowerCase()
                      ) {
                        this.currentUserIsCreatorList.push({
                          testId: test.id,
                          isCreator: true,
                        });
                      } else {
                        this.currentUserIsCreatorList.push({
                          testId: test.id,
                          isCreator: false,
                        });
                      }
                    });
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
      (error) => {
        this.statusMessage = error;
      }
    );
  }

  currentUserIsCreator(testId: number): boolean {
    return (
      this.currentUserIsCreatorList.find(
        (e) => e.testId === testId && e.isCreator === true
      ) != null
    );
  }

  deleteTest(test: ICustomTest): void {
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
        this._customTestService.deleteCustomTest(test.id).subscribe(
          () => {
            const testIndex: number = this.tests.indexOf(test, 0);
            this.tests.splice(testIndex, 1);
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
