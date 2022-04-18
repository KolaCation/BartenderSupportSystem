import { ICustomAnswer } from './custom-answers/ICustomAnswer';

export interface ICustomQuestion {
  id: number;
  statement: string;
  testId: number;
  answers?: ICustomAnswer[];
}
