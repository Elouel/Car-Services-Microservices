import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { env } from 'process';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private loginPath = environment.identityUrl + 'login';
    private logoutPath = environment.identityUrl + 'logout';

    private registerPath = environment.identityUrl + 'register';
    constructor(private http: HttpClient) { }

    public login(data): Observable<any> {
        return this.http.post(this.loginPath, data);
    }

    public logout(): Observable<any> {
        return this.http.post(this.logoutPath, null);
    }

    public register(data): Observable<any> {
        return this.http.post(this.registerPath, data);
    }

    public getCurrentUser(): Observable<any> {
        return this.http.get(environment.identityUrl + localStorage.getItem('currentUserId'));
    }

    public createUser(data): Observable<any> {
        return this.http.post(environment.identityUrl, data);
    }

    public getCurrentUserId(): any {
        return localStorage.getItem('currentUserId');
    }

    isAuthenticated() {
        if (this.getToken()) {
          return true;
        }
        return false;
    }

    public saveToken(token) {
        localStorage.setItem('token', token);
    }

    public getToken() {
        return localStorage.getItem('token');
    }

    public removeToken() {
        return localStorage.clear();
    }
}
