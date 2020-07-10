import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { DepartmentService } from '../services/department.service';
import { Router } from '@angular/router';

@Component({
    selector: 'app-create-department',
    templateUrl: './create-department.component.html',
    styleUrls: ['./create-department.component.css']
})
export class CreateDepartmentComponent implements OnInit {

    public createDepartmentForm: FormGroup;

    constructor(private fb: FormBuilder,
        private router: Router,
        private departmetnService: DepartmentService) {
        this.createDepartmentForm = this.fb.group({
            'departmentName': ['', [Validators.required, Validators.minLength(3)]]
        });
    }

    public ngOnInit() {
    }

    public get departmentName() {
        return this.createDepartmentForm.get('departmentName');
    }

    public createDepartment() {
        this.departmetnService.createDepartment(this.createDepartmentForm.value).subscribe(() =>
            this.router.navigate(['dashboard'])
        );
    }
}
