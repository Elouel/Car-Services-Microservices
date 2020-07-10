import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DepartmentService } from '../services/department.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-create-service',
    templateUrl: './create-service.component.html',
    styleUrls: ['./create-service.component.css']
})
export class CreateServiceComponent {
    public createServiceForm: FormGroup;

    constructor(private fb: FormBuilder,
        private departmentService: DepartmentService,
        private router: Router,
        private route: ActivatedRoute) {
        this.createServiceForm = this.fb.group({
            'serviceName': ['', [Validators.required, Validators.minLength(3)]],
            'price': ['', [Validators.required]],
            'departmentId': [Number(this.route.snapshot.params['id'])],
        });
    }

    get serviceName() {
        return this.createServiceForm.get('password');
    }

    get price() {
        return this.createServiceForm.get('price');
    }

    public createService() {
        this.departmentService.createService(this.createServiceForm.value).subscribe(() =>
            this.router.navigate(['department', this.route.snapshot.params['id']]));
    }

}
