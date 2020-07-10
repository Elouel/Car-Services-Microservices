import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { EmployeeService } from './employee.service';
import { Router, CanActivate, ActivatedRouteSnapshot } from '@angular/router';

@Injectable({
    providedIn: 'root'
})
export class AuthRoleGuardService implements CanActivate {

    constructor(private authService: AuthService, private employeeService: EmployeeService, private router: Router) { }

    public canActivate(route: ActivatedRouteSnapshot): boolean {

      const role = route.data.role;

      if (this.authService.isAuthenticated() &&
          this.employeeService.getRole() === Number(role)) {
          return true;
      } else {
          this.router.navigate(['login']);
          return false;
      }

    }
}
