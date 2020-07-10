import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class VehicleService {

    private vehicleUrl = environment.apiUrl + 'vehicles';
    constructor(
        private http: HttpClient
    ) { }

    public createVehicle(data): Observable<any> {
        return this.http.post(this.vehicleUrl, data);
    }

    public getAll(): Observable<any> {
        return this.http.get(this.vehicleUrl);
    }
}
