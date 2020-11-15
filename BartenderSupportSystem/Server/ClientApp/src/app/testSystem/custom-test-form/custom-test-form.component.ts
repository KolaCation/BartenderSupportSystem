import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormArray } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ErrorHandlerService } from 'src/app/shared/ErrorHandlerService';
import Swal from 'sweetalert2';
import { CustomTestService } from '../custom-test/custom-test.service';
import { ICustomTest } from '../custom-test/ICustomTest';

@Component({
  selector: 'app-custom-test-form',
  templateUrl: './custom-test-form.component.html',
  styleUrls: ['./custom-test-form.component.css']
})
export class CustomTestFormComponent implements OnInit {

  customTestForm: FormGroup;
  customTest: ICustomTest;

  messages = {
    "name": {
      "minlength": "Name must be at least 2 chars long.",
      "maxlength": "Name must not exceed 60 chars.",
      "required": "Name is required."
    },
    "topic": {
      "minlength": "Topic must be at least 2 chars long.",
      "maxlength": "Topic must not exceed 60 chars.",
      "required": "Topic is required."
    },
    "description": {
      "required": "Description is required.",
      "minlength": "Description must be at least 2 chars long.",
      "maxlength": "Description must not exceed 255 chars.",
    },
  }

  formErrors = {
    "name": "",
    "topic": "",
    "description": ""
  }

  constructor(private _formBuilder: FormBuilder, private _customTestService: CustomTestService,
    private _activatedRoute: ActivatedRoute, private _router: Router,
    private _errService: ErrorHandlerService) { }

  ngOnInit(): void {
    this.customTest = {
      id: 0,
      name: null,
      topic: null,
      description: null,
      questions: null
    }
    this.customTestForm = this._formBuilder.group({
      name: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(60)]],
      topic: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(60)]],
      description: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(255)]],
      questions: this._formBuilder.array([this.addQuestionFormGroup()])
    });

    this.customTestForm.valueChanges.subscribe(() => {
      this.validateFormValue(this.customTestForm);
    });

    this._activatedRoute.paramMap.subscribe(params => {
      const customTestId = +params.get('id');
      if (customTestId) {
        this.fillFormWithValuesToEdit(customTestId);
      }
    },
      () => {
        Swal.fire({
          position: 'center',
          icon: 'error',
          title: 'Oops...',
          text: 'Something went wrong!'
        });
      }
    );
    //ngOnInit end
  }

  validateFormValue(formGroup: FormGroup = this.customTestForm) {
    this.formErrors = this._errService.handleClientErrors(formGroup, this.messages);
  }

  addQuestionClick(): void {
    (<FormArray>this.customTestForm.get('questions')).push(this.addQuestionFormGroup());
  }

  addAnswerClick(i: number): void {
    (<FormArray>(<FormGroup>(<FormArray>this.customTestForm.get('questions')).at(i)).get('answers')).push(this.addAnswerFormGroup());
  }

  addQuestionFormGroup(): FormGroup {
    return this._formBuilder.group({
      questionId: [0, [Validators.required]],
      questionStatement: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(60)]],
      answers: this._formBuilder.array([this.addAnswerFormGroup()])
    });
  }

  addAnswerFormGroup(): FormGroup {
    return this._formBuilder.group({
      answerId: [0, [Validators.required]],
      questionId: [0, [Validators.required]],
      answerStatement: ['', [Validators.required]],
      answerIsCorrect: [false, []]
    });
  }

  fillFormWithValuesToEdit(id: number): void {
    this._customTestService.getCustomTest(id).subscribe(
      (customTest: ICustomTest) => {
        Object.assign(this.customTest, customTest);
        this.customTestForm.patchValue({
          name: this.customTest.name,
          topic: this.customTest.topic,
          description: this.customTest.description
        });
        //this.customTestForm.setControl('questions', this.generateFormArray(this.customTest.questions));
      },
      () => {
        Swal.fire({
          position: 'center',
          icon: 'error',
          title: 'Oops...',
          text: 'Something went wrong!'
        });
      }
    );
  }

  /*generateFormArray(questions: IQuestion[]): FormArray {
    let formArr: FormArray = new FormArray([]);
    let questionFormValues = QuestionMapper.toFormValues(questions);
    questionFormValues.forEach((question: any) => {
      let formGroup: FormGroup = this._formBuilder.group({
        questionId: [question.questionId, [Validators.required]],
        componentName: [question.componentName, [Validators.required, CustomValidators.componentExists(this.componentList)]],
        proportionType: [question.proportionType, [Validators.required, CustomValidators.validateProportionType()]],
        proportionValue: [question.proportionValue, [Validators.required, Validators.min(0), Validators.max(10000)]]
      });
      formArr.push(formGroup);
    });
    return formArr;
  }*/

  mapFormValuesToModel(): void {
    this.customTest.name = this.customTestForm.get('name').value;
    this.customTest.topic = this.customTestForm.get('topic').value;
    this.customTest.description = this.customTestForm.get('description').value;
    this.customTest.questions = null;
  }

  handleCreateAction(): void {
    this.mapFormValuesToModel();
    this._customTestService.createCustomTest(this.customTest).subscribe(
      () => {
        this._router.navigate(['/tests']);
        Swal.fire({
          position: 'center',
          icon: 'success',
          title: 'Successfully created!',
          showConfirmButton: false,
          timer: 1500
        });
      },
      () => {
        this.customTestForm.markAllAsTouched();
        this.validateFormValue();
        Swal.fire({
          position: 'center',
          icon: 'error',
          title: 'Oops...',
          text: 'Something went wrong!'
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
          timer: 1500
        })
      },
      error => {
        this.customTestForm.markAllAsTouched();
        this.validateFormValue();
        console.log(error.errors);
        Swal.fire({
          position: 'center',
          icon: 'error',
          title: 'Oops...',
          text: 'Something went wrong!'
        });
      }
    );
  }

  onSubmit() {
    if (this.customTest.id != 0) {
      this.handleEditAction(this.customTest);
    }
    else {
      this.handleCreateAction();
    }
  }

  RemoveQuestion(i: number): void {
    (<FormArray>this.customTestForm.get('questions')).removeAt(i);
  }

  RemoveAnswer(questionIndex: number, answerIndex: number): void {
    (<FormArray>(<FormGroup>(<FormArray>this.customTestForm.get('questions')).at(questionIndex)).get('answers')).removeAt(answerIndex);
  }
}
