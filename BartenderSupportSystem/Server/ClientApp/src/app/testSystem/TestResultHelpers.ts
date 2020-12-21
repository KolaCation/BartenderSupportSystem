import { ICustomTest } from './custom-test/ICustomTest';
import { ICustomQuestion } from './custom-test/custom-questions/ICustomQuestion';
import { ICustomTestResult } from './custom-test-result/ICustomTestResult';
import { ICustomAnswer } from './custom-test/custom-questions/custom-answers/ICustomAnswer';
import { IPickedAnswer } from './custom-test-result/IPickedAnswer';

export class TestResultHelpers {

    public static CreateResultFromTest(test: ICustomTest): ICustomTestResult {
        let custoTestResult: ICustomTestResult;
        custoTestResult.id = 0;
        custoTestResult.customTestId = test.id;
        let pickedAnswers: IPickedAnswer[];
        test.questions.forEach((question: ICustomQuestion) => {
            question.answers.forEach((customAnswer: ICustomAnswer) => {
                let pickedAnswer: IPickedAnswer = {
                    id: 0,
                    customTestResultId: custoTestResult.id,
                    customAnswerId: customAnswer.id,
                    isPicked: customAnswer.isCorrect
                }
                pickedAnswers.push(pickedAnswer);
            });
        });
        custoTestResult.answers = pickedAnswers;
        custoTestResult.personalMark = 0;
        custoTestResult.username = null;
        return custoTestResult;
    }

    public static MapPickedAnswersToTestAnswers(source: ICustomTestResult, dest: ICustomTest): void {
        source.answers.forEach((pickedAnswer: IPickedAnswer) => {
            dest.questions.forEach((question: ICustomQuestion) => {
                let answerToEditIndex: number = question.answers.findIndex(e => e.id === pickedAnswer.id);
                question.answers[answerToEditIndex].isCorrect = pickedAnswer.isPicked;
            });
        });
    }

    public static PrepareTestForPassing(test: ICustomTest): void {
        test.questions.forEach((question: ICustomQuestion) => {
            question.answers.forEach((answer: ICustomAnswer) => answer.isCorrect = false);
        });
    }
}