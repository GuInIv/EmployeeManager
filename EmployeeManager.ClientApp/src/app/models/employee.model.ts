import { Position } from "./position.model";

export class Employee {
  constructor(
    public id?: number,
    public firstName?: string,
    public lastName?: string,
    public salary?: number,
    public hiringDate?: string,
    public terminationDate?: string,
    public position?: Position){
  }
}
