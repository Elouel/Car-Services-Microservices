import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { GarageService } from '../services/garage.service';
import { Router } from '@angular/router';
import { EmployeeService } from '../services/employee.service';

@Component({
    selector: 'app-create-garage',
    templateUrl: './create-garage.component.html',
    styleUrls: ['./create-garage.component.css']
})
export class CreateGarageComponent implements OnInit {


    public garageNameFormControl: FormControl = new FormControl(null, [Validators.required, Validators.minLength(3)]);
    public adminNameControl: FormControl = new FormControl(null, [Validators.required]);

    public createGarageForm: FormGroup = new FormGroup({
        garageName: this.garageNameFormControl,
        adminName: this.adminNameControl,
    });


    public currentUser: any;
    constructor(
        private authService: AuthService,
        private garageService: GarageService,
        private employeeService: EmployeeService,
        private router: Router,
        private changeDetectoreRef: ChangeDetectorRef
    ) { }

    ngOnInit() {
        if (this.garageService.hasAssignedGarage()) {
            this.router.navigate(['dashboard']);
        }

        this.authService.getCurrentUser()
            .subscribe(data => {
                this.currentUser = data;
                this.createGarageForm.controls['adminName'].setValue(data.name);

                this.changeDetectoreRef.markForCheck();
            });
    }

    public createGarage() {
        this.garageService.createGarage(this.createGarageForm.value).subscribe((garage) => {
            this.garageService.setCurrentGarage(garage.id);
            this.employeeService.setCurrentEmployee(garage.employees[0]);
            this.router.navigate(['dashboard']);
        });
    }

    public get garageName() {
        return this.createGarageForm.get('garageName');
    }

    public get adminName() {
        return this.createGarageForm.get('adminName');
    }
}
