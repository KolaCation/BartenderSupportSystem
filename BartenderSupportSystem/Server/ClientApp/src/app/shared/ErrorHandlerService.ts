import { FormArray, FormGroup } from '@angular/forms';

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

    handleServerErrors(formGroupToValidate: FormGroup, errors: any): any {
        for (let formControlName in formGroupToValidate.controls) {
            this.formErrors[formControlName] = "";
        }
        if (errors != null) {
            Object.keys(errors).forEach((key: string) => {
                let formControlName = key.charAt(0).toLowerCase() + key.slice(1);
                this.formErrors[formControlName] = "SERVER VALIDATON: ";
                const messagesForControl = errors[key];
                for (let msg of messagesForControl) {
                    this.formErrors[formControlName] += msg + " ";
                }
            });
        }
        return this.formErrors;
    }
}