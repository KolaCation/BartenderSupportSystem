import { ICustomTest } from './custom-test/ICustomTest';
import { ICustomQuestion } from './custom-test/custom-questions/ICustomQuestion';
import { ICustomTestResult } from './custom-test-result/ICustomTestResult';
import { ICustomAnswer } from './custom-test/custom-questions/custom-answers/ICustomAnswer';
import { IPickedAnswer } from './custom-test-result/IPickedAnswer';
export class TestResultHelpers {
  public static createResultFromTest(
    passedTest: ICustomTest
  ): ICustomTestResult {
    const customTestResult: ICustomTestResult = {
      id: 0,
      customTestId: passedTest.id,
      personalMark: 0,
      userName: null,
      pickedAnswers: null,
    };
    const pickedAnswers: IPickedAnswer[] = new Array<IPickedAnswer>();
    passedTest.questions.forEach((question: ICustomQuestion) => {
      question.answers.forEach((customAnswer: ICustomAnswer) => {
        const pickedAnswer: IPickedAnswer = {
          id: 0,
          customTestResultId: customTestResult.id,
          customAnswerId: customAnswer.id,
          isPicked: customAnswer.isCorrect,
        };
        pickedAnswers.push(pickedAnswer);
      });
    });
    customTestResult.pickedAnswers = pickedAnswers;
    return customTestResult;
  }

  public static setupMarkAndUsername(
    result: ICustomTestResult,
    origin: ICustomTest,
    username: string
  ): void {
    const totalQuestionsCount: number = origin.questions.length;
    let correctAnsweredQuestionsCount = 0;
    origin.questions.forEach((question: ICustomQuestion) => {
      let incorrectAnswersCount = 0;
      question.answers.forEach((answer: ICustomAnswer) => {
        const pickedAnswerIndex: number = result.pickedAnswers.findIndex(
          (e) => e.customAnswerId === answer.id
        );
        if (
          result.pickedAnswers[pickedAnswerIndex].isPicked !== answer.isCorrect
        ) {
          incorrectAnswersCount++;
        }
      });
      if (incorrectAnswersCount === 0) {
        correctAnsweredQuestionsCount++;
      }
    });
    result.personalMark =
      (correctAnsweredQuestionsCount / totalQuestionsCount) * 100;
    result.userName = username;
  }

  public static mapPickedAnswersToTestAnswers(
    result: ICustomTestResult,
    origin: ICustomTest
  ): ICustomTest {
    const testToView: ICustomTest = this.deepCopyTest(origin);
    result.pickedAnswers.forEach((pickedAnswer: IPickedAnswer) => {
      testToView.questions.forEach((question: ICustomQuestion) => {
        const answerToEditIndex: number = question.answers.findIndex(
          (e) => e.id === pickedAnswer.customAnswerId
        );
        if (answerToEditIndex !== -1) {
          question.answers[answerToEditIndex].isCorrect = pickedAnswer.isPicked;
        }
      });
    });
    return testToView;
  }

  public static getIncorrectAnsweredQuestionIds(
    origin: ICustomTest,
    passedTest: ICustomTest
  ): number[] {
    const incorrectAnsweredQuestionIds: number[] = [];
    passedTest.questions.forEach((questionToCompare: ICustomQuestion) => {
      const originQuestion: ICustomQuestion = origin.questions.find(
        (e) => e.id === questionToCompare.id
      );
      for (const answerToCompare of questionToCompare.answers) {
        const originAnswer: ICustomAnswer = originQuestion.answers.find(
          (e) => e.id === answerToCompare.id
        );
        if (answerToCompare.isCorrect !== originAnswer.isCorrect) {
          incorrectAnsweredQuestionIds.push(questionToCompare.id);
          break;
        }
      }
    });
    return incorrectAnsweredQuestionIds;
  }

  public static prepareTestForPassing(origin: ICustomTest): ICustomTest {
    const testToPass: ICustomTest = this.deepCopyTest(origin);
    testToPass.questions.forEach((question: ICustomQuestion) => {
      question.answers.forEach(
        (answer: ICustomAnswer) => (answer.isCorrect = false)
      );
    });

    return testToPass;
  }

  private static deepCopyTest(source: ICustomTest): ICustomTest {
    const testCopy: ICustomTest = {
      id: source.id,
      name: source.name,
      topic: source.topic,
      description: source.description,
      authorUsername: source.authorUsername,
      questions: null,
    };
    const questionsCopy: ICustomQuestion[] = new Array<ICustomQuestion>();
    source.questions.forEach((question: ICustomQuestion) => {
      const questionCopy: ICustomQuestion = {
        id: question.id,
        statement: question.statement,
        testId: question.testId,
        answers: null,
      };
      const answersCopy: ICustomAnswer[] = new Array<ICustomAnswer>();
      question.answers.forEach((answer: ICustomAnswer) => {
        const answerCopy: ICustomAnswer = Object.assign({}, answer);
        answersCopy.push(answerCopy);
      });
      questionCopy.answers = answersCopy;
      questionsCopy.push(questionCopy);
    });
    testCopy.questions = questionsCopy;
    return testCopy;
  }
}
