import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { GarageService } from '../services/garage.service';
import { switchMap } from 'rxjs/operators';
import { EmployeeService } from '../services/employee.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

    public loginForm: FormGroup;

    constructor(private fb: FormBuilder,
        private authService: AuthService,
        private garageService: GarageService,
        private employeeService: EmployeeService,
        private router: Router) {
        this.loginForm = this.fb.group({
            'username': ['', [Validators.required, Validators.minLength(3)]],
            'password': ['', [Validators.required]]
        });
    }

    public ngOnInit() {
    }

    public login() {
        const sub = this.authService.login(this.loginForm.value)
            .pipe(
                switchMap((data) => {
                    this.authService.saveToken(data.token);
                    localStorage.setItem('currentUserId', data.userId);
                    return this.employeeService.getCurrentUserEmployee();
                })
            )
            .subscribe((data) => {
                if (!data) {
                    this.router.navigate(['garage/create']);
                } else {
                    this.garageService.setCurrentGarage(data.garageId);
                    this.employeeService.setCurrentEmployee(data);
                    this.router.navigate(['dashboard']);
                }
            });
    }

    get username() {
        return this.loginForm.get('username');
    }

    get password() {
        return this.loginForm.get('password');
    }
}
