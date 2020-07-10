import { JobStatus } from './roleType';

export interface Job {
    Id: number;
    CreatedByEmployeeId: number;
    CreateByEmployeeName: string;
    AssignedEmployeeId: number;
    AssignedEmployeeName: string;
    JobStatus: JobStatus;
    DeadLine: string;
    CreatedDate: string;
    StratedDate: string;
    FinishedDate: string;
    DepartmentId: number;
    Vehicle: Vehicle;
    PurchasedServices: PurchasedService[];
}

export interface Vehicle {
    Id: number;
    Make: string;
    Model: string;
    Color: string;
    RegistryNumber: string;
 }

 export interface PurchasedService {
    Id: number;
    Price: number;
    Name: string;
 }
