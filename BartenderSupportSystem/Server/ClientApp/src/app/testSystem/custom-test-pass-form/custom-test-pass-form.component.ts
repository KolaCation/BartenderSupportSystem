import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { CustomValidators } from 'src/app/shared/CustomValidators';
import { ErrorHandlerService } from 'src/app/shared/ErrorHandlerService';
import Swal from 'sweetalert2';
import { ICustomAnswer } from '../custom-test/custom-questions/custom-answers/ICustomAnswer';
import { ICustomQuestion } from '../custom-test/custom-questions/ICustomQuestion';
import { CustomTestService } from '../custom-test/custom-test.service';
import { ICustomTest } from '../custom-test/ICustomTest';
import { TestResultHelpers } from '../TestResultHelpers';

@Component({
  selector: 'app-custom-test-pass-form',
  templateUrl: './custom-test-pass-form.component.html',
  styleUrls: ['./custom-test-pass-form.component.css']
})
export class CustomTestPassFormComponent implements OnInit {

  testToPass: ICustomTest;
  testToPassForm: FormGroup;

  constructor(private _formBuilder: FormBuilder, private _testToPassService: CustomTestService,
    private _activatedRoute: ActivatedRoute, private _router: Router,
    private _errService: ErrorHandlerService, private _authorizeService: AuthorizeService) { }

  ngOnInit(): void {
    this.testToPassForm = this._formBuilder.group({
      questions: this._formBuilder.array([])
    });
    this._activatedRoute.paramMap.subscribe(params => {
      const id = +params.get('id');
      this._testToPassService.getCustomTest(id).subscribe((data: ICustomTest) => {
        this.testToPass = data;
        TestResultHelpers.PrepareTestForPassing(this.testToPass);
        this.testToPassForm.setControl('questions', this.fromModelToQuestionFormArray(this.testToPass.questions));
      }, err => {
        console.log(err);
        Swal.fire({
          position: 'center',
          icon: 'error',
          title: 'Oops...',
          text: 'Something went wrong!'
        });
      });
    });
  }


  //#region Mappers

  mapFormValuesToModel(): void {
    this.testToPass.questions = this.fromQuestionFormArrayToModel(this.testToPassForm.get('questions'));
  }

  fromQuestionFormArrayToModel(control: AbstractControl): ICustomQuestion[] {
    if (control && control instanceof FormArray) {
      let values: any[] = (<FormArray>control).value;
      let questionsArr: ICustomQuestion[] = new Array();
      values.forEach(value => {
        let question: ICustomQuestion = {
          id: +value.questionId,
          testId: +this.testToPass.id,
          statement: value.questionStatement,
          answers: this.fromAnswerFormArrayToModel(value.questionId, value.answers)
        };
        questionsArr.push(question);
      });
      return questionsArr;
    }
  }
  fromModelToQuestionFormArray(questions: ICustomQuestion[]): FormArray {
    let formArr: FormArray = new FormArray([]);
    questions.forEach(question => {
      formArr.push(this._formBuilder.group({
        questionId: question.id,
        questionStatement: question.statement,
        answers: this.fromModelToAnswerFormArray(question.answers)
      }));
    });
    return formArr;
  }
  fromModelToAnswerFormArray(answers: ICustomAnswer[]): FormArray {
    let formArr: FormArray = new FormArray([], [CustomValidators.atLeastOneCorrectAnswerExists()]);
    answers.forEach(answer => {
      formArr.push(this._formBuilder.group({
        answerId: answer.id,
        questionId: answer.questionId,
        answerIsCorrect: answer.isCorrect,
        answerStatement: answer.statement
      }));
    });
    return formArr;
  }
  fromAnswerFormArrayToModel(questionId: number, answers: any[]): ICustomAnswer[] {
    let answersArr: ICustomAnswer[] = new Array();
    answers.forEach(value => {
      let answer: ICustomAnswer = {
        id: +value.answerId,
        questionId: +questionId,
        statement: value.answerStatement,
        isCorrect: value.answerIsCorrect
      };
      answersArr.push(answer);
    });
    return answersArr;
  }
  //#endregion
}
