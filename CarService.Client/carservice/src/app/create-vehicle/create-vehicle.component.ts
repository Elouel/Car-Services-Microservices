import { Component } from '@angular/core';
import { FormGroup } from '@angular/forms/src/model';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { VehicleService } from '../services/vehicle.service';

@Component({
    selector: 'app-create-vehicle',
    templateUrl: './create-vehicle.component.html',
    styleUrls: ['./create-vehicle.component.css']
})
export class CreateVehicleComponent {

    public createVehicleForm: FormGroup;

    constructor(
        private fb: FormBuilder,
        private router: Router,
        private vehicleService: VehicleService,
    ) {
        this.createVehicleForm = this.fb.group({
            'make': ['', [Validators.required]],
            'model': ['', [Validators.required]],
            'color': ['', [Validators.required]],
            'registryNumber': ['', [Validators.required]],
        });
    }

    public get make() {
        return this.createVehicleForm.get('make');
    }
    public get model() {
        return this.createVehicleForm.get('model');
    }
    public get color() {
        return this.createVehicleForm.get('color');
    }
    public get registryNumber() {
        return this.createVehicleForm.get('registryNumber');
    }

    public createVehicle() {
        this.vehicleService.createVehicle(this.createVehicleForm.value).subscribe(() =>
            this.router.navigate(['dashboard'])
        );
    }
}
