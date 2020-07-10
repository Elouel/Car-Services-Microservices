import { Component, OnInit, NgModule } from '@angular/core';
import { GarageService } from '../services/garage.service';
import { Router } from '@angular/router';
import { JobService } from '../services/job.service';
import { JobStatus } from '../models/roleType';
import { Job } from '../models/models';

@Component({
    selector: 'app-dashboard-operator',
    templateUrl: './dashboard-operator.component.html',
    styleUrls: ['./dashboard-operator.component.css'],
})

export class DashboardOperatorComponent implements OnInit {

    public garage: any;
    public jobs: Job[];
    public jobStatuses = JobStatus;

    constructor(private garageService: GarageService,
        private jobService: JobService,
        private router: Router) { }

    public ngOnInit() {
        this.fetchGarage();
        this.fetchCurrentUserJobs();
    }

    public navigateToDepartment(id) {
        this.router.navigate(['department', id]);
    }

    public getStatus(jobStatus: number) {
        return this.jobStatuses[jobStatus];
    }

    public isStarted(jobStatus: number) {
        return jobStatus === JobStatus.Started;
    }

    private fetchGarage() {
        this.garageService.getGarage().subscribe(garage => this.garage = garage);
    }

    private fetchCurrentUserJobs() {
        this.jobService.getUserJobs().subscribe(jobs => this.jobs = jobs);
    }

    public finishJob(jobId) {
        this.jobService.changeStatus(jobId, { jobStatus: JobStatus.Finished }).subscribe(() =>
            this.router.navigate(['dashboard'])
        );
    }
}
