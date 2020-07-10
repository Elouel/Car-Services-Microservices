import { Component, OnInit, NgModule } from '@angular/core';
import { EmployeeService } from '../services/employee.service';

@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent {

    constructor(private employeeService: EmployeeService) { }

    public get isAdmin() {
        return this.employeeService.isAdmin();
    }

    public get isSalesman() {
      return this.employeeService.isSalesman();
    }

    public get isOperator() {
      return this.employeeService.isOperator();
    }
}
