import { AbstractControl, FormArray, FormGroup } from '@angular/forms';

export class ErrorHandlerService {

    formErrors = {}

    handleClientErrors(formGroupToValidate: FormGroup, messages: any): any {
        Object.keys(formGroupToValidate.controls).forEach((formControlName: string) => {
            this.formErrors[formControlName] = "";
            const abstractControl = formGroupToValidate.get(formControlName);
            if (abstractControl && abstractControl.invalid && (abstractControl.touched || abstractControl.dirty)) {
                const messagesForControl = messages[formControlName];
                for (let errorKey in abstractControl.errors) {
                    this.formErrors[formControlName] += messagesForControl[errorKey] + " ";
                }
            }

            if (abstractControl && abstractControl instanceof FormGroup) {
                this.handleClientErrors(abstractControl, messages);
            }
        });
        return this.formErrors;
    }
}