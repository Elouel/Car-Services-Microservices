import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AuthorizedComponent } from './layout/authorized/authorized.component';
import { AnonymousComponent } from './layout/anonymous/anonymous.component';
import { AuthGuardService } from './services/auth-guard.service';
import { AuthRoleGuardService } from './services/auth-role-guard.service';
import { CreateDepartmentComponent } from './create-department/create-department.component';
import { CreateEmployeeComponent } from './create-employee/create-employee.component';
import { DepartmentDetailsComponent } from './department-details/department-details.component';
import { CreateServiceComponent } from './create-service/create-service.component';
import { CreateGarageComponent } from './create-garage/create-garage.component';
import { CreateVehicleComponent } from './create-vehicle/create-vehicle.component';
import { CreateJobComponent } from './create-job/create-job.component';

import { RoleType } from './models/roleType';

const appRoutes: Routes = [

    // Site routes goes here
    // {
    //     path: '',
    //     component: SiteLayoutComponent,
    //     children: [
    //         { path: '', component: HomeComponent, pathMatch: 'full' },
    //         { path: 'about', component: AboutComponent }
    //     ]
    // },

    // App routes goes here here
    {
        path: '',
        component: AuthorizedComponent,
        children: [
            { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuardService] },
            { path: 'department/create', component: CreateDepartmentComponent, canActivate: [AuthGuardService],
              data: {
                role: RoleType.Admin
              }
            },
            { path: 'garage/create', component: CreateGarageComponent, canActivate: [AuthGuardService] },
            { path: 'department/:id', component: DepartmentDetailsComponent, canActivate: [AuthGuardService] },
            { path: 'employee/create', component: CreateEmployeeComponent, canActivate: [AuthGuardService] },
            { path: 'department/:id/service/create', component: CreateServiceComponent, canActivate: [AuthGuardService] },
            { path: 'vehicle/create', component: CreateVehicleComponent, canActivate: [AuthGuardService]},
            { path: 'department/:id/job/create', component: CreateJobComponent, canActivate: [AuthGuardService]},
        ]
    },
    {
        path: '',
        component: AnonymousComponent,
        children: [
            { path: 'login', component: LoginComponent },
            { path: 'register', component: RegisterComponent },
        ]
    },

    // no layout routes
    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];


@NgModule({
    imports: [RouterModule.forRoot(appRoutes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
