import { Component, OnInit } from '@angular/core';
import { VehicleService } from '../services/vehicle.service';
import { JobService } from '../services/job.service';
import { DepartmentService } from '../services/department.service';
import { FormGroup } from '@angular/forms/src/model';
import { FormBuilder, FormControl, FormArray, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-create-job',
    templateUrl: './create-job.component.html',
    styleUrls: ['./create-job.component.css']
})
export class CreateJobComponent implements OnInit {

    public vehicles: any;
    public department: any;
    public createJobForm: FormGroup;

    constructor(
        private vehicleService: VehicleService,
        private jobService: JobService,
        private departmentService: DepartmentService,
        private fb: FormBuilder,
        private router: Router,
        private route: ActivatedRoute
    ) { }

    ngOnInit() {
        this.vehicleService.getAll().subscribe(v => this.vehicles = v);
        this.departmentService
            .getDepartment(this.route.snapshot.paramMap.get('id'))
            .subscribe(department => this.department = department)
            .add();

        this.createJobForm = this.fb.group({
            'departmentId': [Number(this.route.snapshot.params['id'])],
            'vehicleId': ['', [Validators.required]],
            'purchasedServices': this.fb.array([]),
            'deadline': ['', [Validators.required]]
        });
    }

    onCheckboxChange(e) {
      const checkArray: FormArray = this.createJobForm.get('purchasedServices') as FormArray;

      if (e.target.checked) {
        checkArray.push(new FormControl(Number(e.target.value)));
      } else {
        let i = 0;
        checkArray.controls.forEach((item: FormControl) => {
          if (item.value === e.target.value) {
            checkArray.removeAt(i);
            return;
          }
          i++;
        });
      }
    }

    public get vehicleId() {
        return this.createJobForm.get('vehicleId');
    }

    get purchasedServices() {
        return this.createJobForm.get('purchasedServices');
    }

    public get deadline() {
      return this.createJobForm.get('deadline');
    }

    public createJob() {
      this.jobService.createJob(this.createJobForm.value).subscribe(() =>
          this.router.navigate(['dashboard'])
      );
    }
}
