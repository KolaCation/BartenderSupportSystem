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
      username: null,
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
    result.username = username;
  }

  public static mapPickedAnswersToTestAnswers(
    origin: ICustomTestResult,
    dest: ICustomTest
  ): void {
    origin.pickedAnswers.forEach((pickedAnswer: IPickedAnswer) => {
      dest.questions.forEach((question: ICustomQuestion) => {
        const answerToEditIndex: number = question.answers.findIndex(
          (e) => e.id === pickedAnswer.id
        );
        question.answers[answerToEditIndex].isCorrect = pickedAnswer.isPicked;
      });
    });
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
