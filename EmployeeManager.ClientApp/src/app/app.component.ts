import { Component } from '@angular/core';
import { DataService } from './services/data.service';
import { Employee } from './models/employee.model';
import { Position } from './models/position.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  providers: [DataService]
})
export class AppComponent {

  constructor(private dataService: DataService) { }

  get employees(): Employee[] {
    return this.dataService.employees;
  }

  get employee(): Employee {
    return this.dataService.employee;
  }

  get positions(): Position[] {
    return this.dataService.positions;
  }

  get position(): Position {
    return this.dataService.position;
  }
  
  createEmployee() {
    this.dataService.createEmployee(this.employee);
  }

  createPosition() {
    this.dataService.createPosition(this.position);
  }

  deleteEmployee() {
    this.dataService.deleteEmployee(1);
  }

  deletePosition() {
    this.dataService.deletePosition(4);
  } 
}
