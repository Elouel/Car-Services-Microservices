import { Component, OnInit, NgModule } from '@angular/core';
import { GarageService } from '../services/garage.service';
import { Router } from '@angular/router';
import { EmployeeService } from '../services/employee.service';
import { JobService } from '../services/job.service';
import { Job } from '../models/models';
import { JobStatus } from '../models/roleType';

@Component({
    selector: 'app-dashboard-salesman',
    templateUrl: './dashboard-salesman.component.html',
    styleUrls: ['./dashboard-salesman.component.css'],
})

export class DashboardSalesmanComponent implements OnInit {

    public garage: any;
    public jobStatuses = JobStatus;
    public jobs: Job[];

    constructor(private garageService: GarageService,
        private jobService: JobService,
        private router: Router) { }

    public ngOnInit() {
      this.fetchGarage();
      this.fetchCurrentUserJobs();
    }

    public GetStatus(jobStatus: number ) {
        return this.jobStatuses[jobStatus];
    }

    private fetchCurrentUserJobs() {
      this.jobService.getUserJobs().subscribe(jobs => this.jobs = jobs);

      /**
       * test jobs listing
       */
      /*this.jobs = [{
        "id": 22,
        "createdDate": "",
        "deadline": "",
        "jobStatus": "started",
        "vehicle": {
          "make": "test make",
          "model": "test model",
          "registryNumber": "3232132"
        }
      }];*/

    }

    public navigateToCreateJob(id) {
      this.router.navigate(['department', id, 'job', 'create']);
    }

    private fetchGarage() {
        this.garageService.getGarage().subscribe(garage => this.garage = garage);
    }
}
