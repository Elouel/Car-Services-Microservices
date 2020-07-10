import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { ReactiveFormsModule } from '@angular/forms';
import { RegisterComponent } from './register/register.component';
import { AuthService } from './services/auth.service';
import { AuthorizedComponent } from './layout/authorized/authorized.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { DashboardOperatorComponent } from './dashboard-operator/dashboard-operator.component';
import { DashboardAdminComponent } from './dashboard-admin/dashboard-admin.component';
import { DashboardSalesmanComponent } from './dashboard-salesman/dashboard-salesman.component';
import { AnonymousComponent } from './layout/anonymous/anonymous.component';
import { CreateDepartmentComponent } from './create-department/create-department.component';
import { GarageService } from './services/garage.service';
import { DepartmentService } from './services/department.service';
import { TokenInterceptorService } from './services/token-interceptor.service';
import { AuthGuardService } from './services/auth-guard.service';
import { CreateEmployeeComponent } from './create-employee/create-employee.component';
import { EmployeeService } from './services/employee.service';
import { DepartmentDetailsComponent } from './department-details/department-details.component';
import { CreateServiceComponent } from './create-service/create-service.component';
import { CreateGarageComponent } from './create-garage/create-garage.component';
import { EnumToArrayPipe } from './pipes/enumPipe';
import { CreateVehicleComponent } from './create-vehicle/create-vehicle.component';
import { VehicleService } from './services/vehicle.service';
import { CreateJobComponent } from './create-job/create-job.component';
import { JobService } from './services/job.service';

@NgModule({
    declarations: [
        AppComponent,
        LoginComponent,
        RegisterComponent,
        AuthorizedComponent,
        DashboardComponent,
        DashboardOperatorComponent,
        DashboardSalesmanComponent,
        DashboardAdminComponent,
        AnonymousComponent,
        CreateDepartmentComponent,
        CreateEmployeeComponent,
        DepartmentDetailsComponent,
        CreateServiceComponent,
        CreateGarageComponent,
        EnumToArrayPipe,
        CreateVehicleComponent,
        CreateJobComponent,
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        ReactiveFormsModule,
        HttpClientModule,
    ],
    providers: [
        AuthService,
        GarageService,
        DepartmentService,
        TokenInterceptorService,
        EmployeeService,
        VehicleService,
        JobService,
        AuthGuardService,
        {
            provide: HTTP_INTERCEPTORS,
            useClass: TokenInterceptorService,
            multi: true
        }
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
