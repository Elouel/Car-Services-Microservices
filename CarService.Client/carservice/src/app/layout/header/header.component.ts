import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.css']
})
export class HeaderComponent {

    constructor(private authService: AuthService) { }

    public get hasGarage() {
        return localStorage.getItem('currentGarageId') !== 'undefined';
    }

    public logout() {
        this.authService.removeToken();
    }
}
