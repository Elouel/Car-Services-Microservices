import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class DepartmentService {

    private departmentPath = environment.apiUrl + 'department';
    private getDepartmentsForGarageUrl = environment.apiUrl + 'garage/departments';

    constructor(private http: HttpClient) { }

    public createService(data): Observable<any> {
        return this.http.post(environment.apiUrl + 'service', data);
    }

    public getDepartment(id): Observable<any> {
        return this.http.get(this.departmentPath + '/' + id);
    }

    public createDepartment(data): Observable<any> {
        data = { DepartmentName: data.departmentName, GarageId: Number(localStorage.getItem('currentGarageId')) };
        return this.http.post(this.departmentPath, data);
    }

    public getDepartments(): Observable<any> {
        return this.http.get(this.getDepartmentsForGarageUrl + '/' + Number(localStorage.getItem('currentGarageId')));
    }
}
