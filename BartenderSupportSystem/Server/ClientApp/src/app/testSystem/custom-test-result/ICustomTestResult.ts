import { IPickedAnswer } from './IPickedAnswer';

export interface ICustomTestResult {
  id: number;
  customTestId: number;
  pickedAnswers?: IPickedAnswer[];
  username: string;
  personalMark: number;
}
