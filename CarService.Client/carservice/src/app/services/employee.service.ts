import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';
import { RoleType } from '../models/roleType';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

    private employeePath = environment.apiUrl + 'employee';

    constructor(private http: HttpClient, private authService: AuthService) { }

    public createEmployee(data): Observable<any> {
        data.departmentId = Number(data.departmentId);
        data.role = Number(data.role);
        return this.http.post(this.employeePath, data);
    }

    public getCurrentUserEmployee(): Observable<any> {
        return this.http.get(environment.apiUrl + 'employee/' + this.authService.getCurrentUserId());
    }

    public setCurrentEmployee(data) {
        localStorage.setItem('currentEmployeeId', data.employeeId);
        localStorage.setItem('currentEmployeeRole', data.role);
        localStorage.setItem('currentEmployeeRoleDisplayName', data.roleDisplayName);
        localStorage.setItem('currentEmployeeName', data.name);
    }

    public getRole() {
      return Number(localStorage.getItem('currentEmployeeRole'));
    }

    public isAdmin() {
        return Number(localStorage.getItem('currentEmployeeRole')) === Number(RoleType.Admin);
    }

    public isSalesman() {
      return Number(localStorage.getItem('currentEmployeeRole')) === Number(RoleType.Salesmen);
    }

    public isOperator() {
      return Number(localStorage.getItem('currentEmployeeRole')) === Number(RoleType.Operator);
    }
}
