import { IPickedAnswer } from './IPickedAnswer';

export interface ICustomTestResult {
  id: number;
  customTestId: number;
  pickedAnswers?: IPickedAnswer[];
  userName: string;
  personalMark: number;
}
