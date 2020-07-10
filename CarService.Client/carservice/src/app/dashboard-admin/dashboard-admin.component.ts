import { Component, OnInit, NgModule } from '@angular/core';
import { GarageService } from '../services/garage.service';
import { Router } from '@angular/router';
import { EmployeeService } from '../services/employee.service';
import { JobStatus } from '../models/roleType';

@Component({
    selector: 'app-dashboard-admin',
    templateUrl: './dashboard-admin.component.html',
    styleUrls: ['./dashboard-admin.component.css'],
})
export class DashboardAdminComponent implements OnInit {

    public garage: any;

    constructor(private garageService: GarageService,
        private employeeService: EmployeeService,
        private router: Router) { }

    public ngOnInit() {
      this.fetchGarage();
    }

    public navigateToDepartment(id) {
        this.router.navigate(['department', id]);
    }

    public navigateToVehicleCreate() {
        this.router.navigate(['vehicle/create']);
    }

    private fetchGarage() {
        this.garageService.getGarage().subscribe(garage => this.garage = garage);
        this.employeeService.getCurrentUserEmployee().subscribe((data) => {
            this.employeeService.setCurrentEmployee(data);
        });
    }
}
