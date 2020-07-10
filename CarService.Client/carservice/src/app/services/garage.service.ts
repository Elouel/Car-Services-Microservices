import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
    providedIn: 'root'
})
export class GarageService {

    private garageUrl = environment.apiUrl + 'garage';

    constructor(private http: HttpClient) { }

    public getGarage(): Observable<any> {
        return this.http.get(this.garageUrl + '/' + Number(localStorage.getItem('currentGarageId')));
    }

    public createGarage(data): Observable<any> {
        return this.http.post(this.garageUrl, data);
    }

    public getDefaultGarage(): Observable<any> {
        return this.http.get(this.garageUrl + '/defaultGarage/' + localStorage.getItem('currentUserId'));
    }

    public setCurrentGarage(id) {
        localStorage.setItem('currentGarageId', id);
    }

    public hasAssignedGarage() {
        return localStorage.getItem('currentGarageId') !== 'undefined' && localStorage.getItem('currentGarageId') !== null;
    }
}
