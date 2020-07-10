import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';

@Component({
    selector: 'app-header',
    templateUrl: './authorized.component.html',
    styleUrls: ['./authorized.component.css']
})
export class AuthorizedComponent {

    public username: String;
    public role: String;

    constructor(private authService: AuthService) {
      this.username = localStorage.getItem(`currentEmployeeName`);
      this.role = localStorage.getItem(`currentEmployeeRoleDisplayName`);
    }

    public get hasGarage() {
        return localStorage.getItem('currentGarageId') !== 'undefined';
    }

    public logout() {
        this.authService.removeToken();
    }
}
