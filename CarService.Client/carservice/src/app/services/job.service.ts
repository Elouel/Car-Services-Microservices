import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Job } from '../models/models';

@Injectable({
  providedIn: 'root'
})
export class JobService {
    private jobUrl = environment.jobSchedularGateway + 'job';

    constructor(private http: HttpClient) { }

    public getUserJobs(): Observable<Job[]> {
      return this.http.get<Job[]>(this.jobUrl);
    }

    public getDepartmentJobs(departmentId, status): Observable<Job[]> {
      return this.http.get<Job[]>(this.jobUrl + '/department/' + departmentId);
    }

    public createJob(data): Observable<any> {
        data.vehicleId = Number(data.vehicleId);
        return this.http.post(this.jobUrl, data);
    }

    public changeStatus(jobId, data): Observable<any> {
      const jobUpdateUrl = this.jobUrl + '/' + jobId;

      return this.http.put(jobUpdateUrl, data);
    }
}
