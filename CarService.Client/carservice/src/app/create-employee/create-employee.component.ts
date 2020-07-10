import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { DepartmentService } from '../services/department.service';
import { EmployeeService } from '../services/employee.service';
import { AuthService } from '../services/auth.service';
import { RoleType } from '../models/roleType';
import { EnumToArrayPipe } from '../pipes/enumPipe';

@Component({
    selector: 'app-create-employee',
    templateUrl: './create-employee.component.html',
    styleUrls: ['./create-employee.component.css'],
    providers: [EnumToArrayPipe],
})
export class CreateEmployeeComponent implements OnInit {

    public createUserForm: FormGroup;
    public roles = RoleType;
    public createEmployeeForm: FormGroup;
    public departments: any;
    public userCreated = false;

    constructor(private fb: FormBuilder,
        private departmentService: DepartmentService,
        private router: Router,
        private authService: AuthService,
        private employeeService: EmployeeService,
        private changeDetector: ChangeDetectorRef) {

        this.createUserForm = this.fb.group({
            'username': ['', [Validators.required, Validators.minLength(3)]],
            'email': ['', [Validators.required]],
            'password': ['', [Validators.required, Validators.minLength(6)]],
        });


    }

    public ngOnInit() {
        this.departmentService.getDepartments().subscribe(d => {
            this.departments = d;
            this.changeDetector.markForCheck();
        });
    }

    get username() {
        return this.createUserForm.get('username');
    }

    get email() {
        return this.createUserForm.get('email');
    }

    get password() {
        return this.createUserForm.get('password');
    }
    get employeName() {
        return this.createEmployeeForm.get('username');
    }

    get departmentId() {
        return this.createEmployeeForm.get('departmentId');
    }

    get role() {
        return this.createEmployeeForm.get('role');
    }

    public createUser() {
        this.authService.createUser(this.createUserForm.value).subscribe(data => {
            this.createEmployeeForm = this.fb.group({
                'userId': data.id,
                'username': data.name,
                'garageId': [Number(localStorage.getItem('currentGarageId'))],
                'role': ['', Validators.required],
                'departmentId': ['', [Validators.required]],
            });

            this.userCreated = true;
            this.changeDetector.markForCheck();
        });
    }

    public createEmployee() {
        this.employeeService.createEmployee(this.createEmployeeForm.value).subscribe((x) =>
            this.router.navigate(['dashboard']));
    }
}
