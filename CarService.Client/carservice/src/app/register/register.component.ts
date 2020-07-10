import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { EmployeeService } from '../services/employee.service';


@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css']
})
export class RegisterComponent {

    public registerForm: FormGroup;

    constructor(private fb: FormBuilder,
        private authService: AuthService,
        private router: Router) {
        this.registerForm = this.fb.group({
            'username': ['', [Validators.required, Validators.minLength(3)]],
            'email': ['', [Validators.required]],
            'password': ['', [Validators.required, Validators.minLength(6)]],
        });
    }

    public register() {
        const sub = this.authService.register(this.registerForm.value)
        .subscribe((data) => {
            this.authService.saveToken(data.token);
            localStorage.setItem('currentUserId', data.userId);
            this.router.navigate(['garage/create']);
            if (!!sub) {
                sub.unsubscribe();
            }
        });
    }

    get username() {
        return this.registerForm.get('username');
    }

    get email() {
        return this.registerForm.get('email');
    }

    get password() {
        return this.registerForm.get('password');
    }
}
