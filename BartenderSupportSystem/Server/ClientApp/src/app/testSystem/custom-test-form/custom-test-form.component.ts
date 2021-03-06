import { Component, OnInit } from '@angular/core';
import {
  FormGroup,
  FormBuilder,
  Validators,
  FormArray,
  AbstractControl,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { CustomValidators } from 'src/app/shared/CustomValidators';
import { ErrorHandlerService } from 'src/app/shared/ErrorHandlerService';
import Swal from 'sweetalert2';
import { ICustomAnswer } from '../custom-test/custom-questions/custom-answers/ICustomAnswer';
import { ICustomQuestion } from '../custom-test/custom-questions/ICustomQuestion';
import { CustomTestService } from '../custom-test/custom-test.service';
import { ICustomTest } from '../custom-test/ICustomTest';

@Component({
  selector: 'app-custom-test-form',
  templateUrl: './custom-test-form.component.html',
  styleUrls: ['./custom-test-form.component.css'],
})
export class CustomTestFormComponent implements OnInit {
  customTestForm: FormGroup;
  customTest: ICustomTest;
  currentUserIsCreator = false;

  messages = {
    name: {
      minlength: 'Name must be at least 2 chars long.',
      maxlength: 'Name must not exceed 60 chars.',
      required: 'Name is required.',
    },
    topic: {
      minlength: 'Topic must be at least 2 chars long.',
      maxlength: 'Topic must not exceed 60 chars.',
      required: 'Topic is required.',
    },
    description: {
      required: 'Description is required.',
      minlength: 'Description must be at least 2 chars long.',
      maxlength: 'Description must not exceed 255 chars.',
    },
    questions: {
      arrayminlength: 'Test must have at least 4 questions.',
      arraymaxlength: 'Test must not exceed 20 questions.',
    },
  };

  formErrors = {
    name: '',
    topic: '',
    description: '',
    questions: '',
  };

  constructor(
    private _formBuilder: FormBuilder,
    private _customTestService: CustomTestService,
    private _activatedRoute: ActivatedRoute,
    private _router: Router,
    private _errService: ErrorHandlerService,
    private _authorizeService: AuthorizeService
  ) {}

  ngOnInit(): void {
    this.customTest = {
      id: 0,
      name: null,
      topic: null,
      description: null,
      questions: null,
      authorUsername: null,
    };
    this.customTestForm = this._formBuilder.group({
      name: [
        '',
        [
          Validators.required,
          Validators.minLength(2),
          Validators.maxLength(60),
        ],
      ],
      topic: [
        '',
        [
          Validators.required,
          Validators.minLength(2),
          Validators.maxLength(60),
        ],
      ],
      description: [
        '',
        [
          Validators.required,
          Validators.minLength(2),
          Validators.maxLength(255),
        ],
      ],
      questions: this._formBuilder.array(
        [this.addQuestionFormGroup()],
        [
          CustomValidators.validateArrayMinLength(4),
          CustomValidators.validateArrayMaxLength(20),
        ]
      ),
    });

    this.customTestForm.valueChanges.subscribe(() => {
      this.validateFormValue(this.customTestForm);
    });

    this._activatedRoute.paramMap.subscribe(
      (params) => {
        const customTestId = +params.get('id');
        if (customTestId) {
          this.fillFormWithValuesToEdit(customTestId);
        } else {
          this.currentUserIsCreator = true;
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
    //ngOnInit end
  }

  validateFormValue(formGroup: FormGroup = this.customTestForm): void {
    this.formErrors = this._errService.handleClientErrors(
      formGroup,
      this.messages
    );
  }

  onSubmit(): void {
    if (this.customTest.id != 0) {
      this.handleEditAction(this.customTest);
    } else {
      this.handleCreateAction();
    }
  }

  //#region Actions
  handleCreateAction(): void {
    this.mapFormValuesToModel();
    console.log(this.customTest);
    this._customTestService.createCustomTest(this.customTest).subscribe(
      () => {
        this._router.navigate(['/tests']);
        Swal.fire({
          position: 'center',
          icon: 'success',
          title: 'Successfully created!',
          showConfirmButton: false,
          timer: 1500,
        });
      },
      () => {
        this.customTestForm.markAllAsTouched();
        this.validateFormValue();
        Swal.fire({
          position: 'center',
          icon: 'error',
          title: 'Oops...',
          text: 'Something went wrong!',
        });
      }
    );
  }

  handleEditAction(customTest: ICustomTest): void {
    this.mapFormValuesToModel();
    this._customTestService.updateCustomTest(customTest).subscribe(
      () => {
        this._router.navigate(['/tests']);
        Swal.fire({
          position: 'center',
          icon: 'success',
          title: 'Successfully edited!',
          showConfirmButton: false,
          timer: 1500,
        });
      },
      () => {
        this.customTestForm.markAllAsTouched();
        this.validateFormValue();
        Swal.fire({
          position: 'center',
          icon: 'error',
          title: 'Oops...',
          text: 'Something went wrong!',
        });
      }
    );
  }
  //#endregion

  //#region Events
  removeQuestion(i: number): void {
    (<FormArray>this.customTestForm.get('questions')).removeAt(i);
  }

  removeAnswer(questionIndex: number, answerIndex: number): void {
    (<FormArray>(
      (<FormGroup>(
        (<FormArray>this.customTestForm.get('questions')).at(questionIndex)
      )).get('answers')
    )).removeAt(answerIndex);
  }

  addQuestionClick(): void {
    (<FormArray>this.customTestForm.get('questions')).push(
      this.addQuestionFormGroup()
    );
  }

  addAnswerClick(i: number): void {
    (<FormArray>(
      (<FormGroup>(<FormArray>this.customTestForm.get('questions')).at(i)).get(
        'answers'
      )
    )).push(this.addAnswerFormGroup());
  }

  addQuestionFormGroup(): FormGroup {
    return this._formBuilder.group({
      questionId: [0, [Validators.required]],
      questionStatement: [
        '',
        [
          Validators.required,
          Validators.minLength(2),
          Validators.maxLength(60),
        ],
      ],
      answers: this._formBuilder.array(
        [this.addAnswerFormGroup()],
        [
          CustomValidators.validateArrayMinLength(2),
          CustomValidators.validateArrayMaxLength(6),
          CustomValidators.atLeastOneCorrectAnswerExists(),
        ]
      ),
    });
  }

  addAnswerFormGroup(): FormGroup {
    return this._formBuilder.group({
      answerId: [0, [Validators.required]],
      questionId: [0, [Validators.required]],
      answerStatement: [
        '',
        [
          Validators.required,
          Validators.minLength(1),
          Validators.maxLength(60),
        ],
      ],
      answerIsCorrect: [false, []],
    });
  }
  //#endregion

  //#region Mappers
  fillFormWithValuesToEdit(id: number): void {
    this._customTestService.getCustomTest(id).subscribe(
      (customTest: ICustomTest) => {
        this._authorizeService.getUserName().subscribe((name) => {
          if (customTest.authorUsername.toLowerCase() === name.toLowerCase()) {
            this.currentUserIsCreator = true;
            Object.assign(this.customTest, customTest);
            this.customTestForm.patchValue({
              name: this.customTest.name,
              topic: this.customTest.topic,
              description: this.customTest.description,
            });
            this.customTestForm.setControl(
              'questions',
              this.fromModelToQuestionFormArray(this.customTest.questions)
            );
          } else {
            this.currentUserIsCreator = false;
            this._router.navigate(['/tests']);
          }
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
  }

  mapFormValuesToModel(): void {
    this.customTest.name = this.customTestForm.get('name').value;
    this.customTest.topic = this.customTestForm.get('topic').value;
    this.customTest.description = this.customTestForm.get('description').value;
    this.customTest.questions = this.fromQuestionFormArrayToModel(
      this.customTestForm.get('questions')
    );
    this._authorizeService.getUserName().subscribe((name) => {
      this.customTest.authorUsername = name;
      this.currentUserIsCreator = true;
    });
  }

  fromQuestionFormArrayToModel(control: AbstractControl): ICustomQuestion[] {
    if (control && control instanceof FormArray) {
      const values: any[] = (<FormArray>control).value;
      const questionsArr: ICustomQuestion[] = [];
      values.forEach((value) => {
        const question: ICustomQuestion = {
          id: +value.questionId,
          testId: +this.customTest.id,
          statement: value.questionStatement,
          answers: this.fromAnswerFormArrayToModel(
            value.questionId,
            value.answers
          ),
        };
        questionsArr.push(question);
      });
      return questionsArr;
    }
  }
  fromModelToQuestionFormArray(questions: ICustomQuestion[]): FormArray {
    const formArr: FormArray = new FormArray(
      [],
      [
        CustomValidators.validateArrayMinLength(4),
        CustomValidators.validateArrayMaxLength(20),
      ]
    );
    questions.forEach((question) => {
      formArr.push(
        this._formBuilder.group({
          questionId: [question.id, [Validators.required]],
          questionStatement: [
            question.statement,
            [
              Validators.required,
              Validators.minLength(2),
              Validators.maxLength(60),
            ],
          ],
          answers: this.fromModelToAnswerFormArray(question.answers),
        })
      );
    });
    return formArr;
  }
  fromModelToAnswerFormArray(answers: ICustomAnswer[]): FormArray {
    const formArr: FormArray = new FormArray(
      [],
      [
        CustomValidators.validateArrayMinLength(2),
        CustomValidators.validateArrayMaxLength(6),
        CustomValidators.atLeastOneCorrectAnswerExists(),
      ]
    );
    answers.forEach((answer) => {
      formArr.push(
        this._formBuilder.group({
          answerId: [answer.id, [Validators.required]],
          questionId: [answer.questionId, [Validators.required]],
          answerIsCorrect: answer.isCorrect,
          answerStatement: [
            answer.statement,
            [
              Validators.required,
              Validators.minLength(1),
              Validators.maxLength(60),
            ],
          ],
        })
      );
    });
    return formArr;
  }
  fromAnswerFormArrayToModel(
    questionId: number,
    answers: any[]
  ): ICustomAnswer[] {
    const answersArr: ICustomAnswer[] = [];
    answers.forEach((value) => {
      const answer: ICustomAnswer = {
        id: +value.answerId,
        questionId: +questionId,
        statement: value.answerStatement,
        isCorrect: value.answerIsCorrect,
      };
      answersArr.push(answer);
    });
    return answersArr;
  }
  //#endregion
}
