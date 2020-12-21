import { IPickedAnswer } from "./IPickedAnswer";

export interface ICustomTestResult {
    id: number,
    customTestId: number,
    answers?: IPickedAnswer[],
    username: string,
    personalMark: number
}