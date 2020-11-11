import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Employee } from '../models/employee.model';
import { Position } from "../models/position.model";

@Injectable()
export class DataService {

  private employeesUrl = "/api/employees";
  private positionsUrl = "/api/positions";

  employee: Employee = new Employee();
  position: Position = new Position();
  employees: Employee[];
  positions: Position[];

  constructor(private http: HttpClient) {
    this.getEmployees(true);
    this.getPositions();
  }

  getEmployee(id: number) {
    this.http.get<Employee>(`${this.employeesUrl}/{id}`)
      .subscribe(e => this.employee = e);
  }

  getEmployees(related = false) {
    return this.http.get<Employee[]>(`${this.employeesUrl}`, { params: new HttpParams().append('related', `${related}`) })
      .subscribe(empls => {
        this.employees = empls
      });
  }

  createEmployee(empl: Employee) {
    this.http.post<number>(`${this.employeesUrl}/GetEmployeeByParam`,empl)
      .subscribe(id => {
        if (id == 0) {
          return this.http.post<number>(this.employeesUrl, empl).subscribe(() => this.getEmployees(true));
        }
        else {
          return this.http.put(`${this.employeesUrl}/${id}`, empl).subscribe(() => this.getEmployees(true));
        }
      });
  }

  deleteEmployee(id: number) {
    this.http.delete(`${this.employeesUrl}/${id}`)
      .subscribe(() => this.getEmployees());
  }

  deletePosition(id: number) {
    this.http.delete(`${this.positionsUrl}/${id}`)
      .subscribe(() => {
        this.getEmployees(true);
      });
  }

  getPositions() {
    this.http.get<Position[]>(this.positionsUrl)
      .subscribe(pos => this.positions = pos);
  }

  createPosition(pos: Position) {
    let data = {
      name: pos.name
    };
    return this.http.post<number>(this.positionsUrl, data)
      .subscribe(()=> this.getPositions());
  }
}
