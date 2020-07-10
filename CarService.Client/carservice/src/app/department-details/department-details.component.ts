import { Component, OnInit } from '@angular/core';
import { DepartmentService } from '../services/department.service';
import { ActivatedRoute, Router } from '@angular/router';
import { EmployeeService } from '../services/employee.service';
import { JobService } from '../services/job.service';
import { JobStatus } from '../models/roleType';
import { Job } from '../models/models';

@Component({
    selector: 'app-department-details',
    templateUrl: './department-details.component.html',
    styleUrls: ['./department-details.component.css']
})
export class DepartmentDetailsComponent implements OnInit {

    public department: any;
    public jobs: Job[];
    public jobStatuses = JobStatus;

    constructor(private departmentService: DepartmentService,
        private employeeService: EmployeeService,
        private jobService: JobService,
        private router: Router,
        private route: ActivatedRoute) { }

    ngOnInit() {
        const currentDepartmentId = this.route.snapshot.paramMap.get('id');

        this.departmentService
            .getDepartment(currentDepartmentId)
            .subscribe(department => this.department = department);

        this.fetchDepartmentJobs(currentDepartmentId);
    }
    public GetStatus(jobStatus: number) {
        return this.jobStatuses[jobStatus];
    }

    public IsNotStarted(jobStatus: JobStatus) {
        return jobStatus === JobStatus.Pending;
    }

    private fetchDepartmentJobs(departmentId) {

        this.jobService
            .getDepartmentJobs(departmentId, this.isOperator ? 'pending' : '')
            .subscribe(jobs => this.jobs = jobs);
    }

    public get isAdmin() {
        return this.employeeService.isAdmin();
    }

    public get isOperator() {
        return this.employeeService.isOperator();
    }

    public navigateToCreateService() {
        this.router.navigate(['department', this.department.id, 'service', 'create']);
    }

    public navigateToCreateJob() {
        this.router.navigate(['department', this.department.id, 'job', 'create']);
    }

    public startJob(jobId) {
        this.jobService.changeStatus(jobId, { jobStatus: JobStatus.Started }).subscribe(() =>
            this.router.navigate(['dashboard'])
        );
    }
}
