import { ICustomQuestion } from "./custom-questions/ICustomQuestion";

export interface ICustomTest {
    id: number,
    name: string,
    topic: string,
    description: string,
    questions?: ICustomQuestion[]
    authorUsername: string
}