import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { UserLogin } from '../models/user-login';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class MyService {

  private authServer = 'https://localhost:5189/api/';
  private webApiServer = 'https://localhost:7101/api/';

  constructor(private http: HttpClient, private authService: AuthService) { }

  getAuth(user: UserLogin): Observable<any> {
    return this.http.post<any>(`${this.authServer}user/login`, user).pipe(
      tap(response => {
        if (response && response.token) {
          this.authService.setToken(response.token);
        }
      }),
      catchError(this.handleError)
    );
  }

  getPersons(): Observable<any[]> {
    return this.http.get<any[]>(this.webApiServer + 'person')
      .pipe(
        catchError(this.handleError)
      );
  }

  getPersonById(id: string): Observable<any> {
    return this.http.get<any>(`${this.webApiServer}person/${id}`)
      .pipe(
        catchError(this.handleError)
      );
  }

  addPerson(person: { name: string; cpfNumber: number; dateOfBirth: Date; isActive: boolean; contacts: [] }): Observable<any> {
    const headers = { 'Authorization': `Bearer ${this.authService.getToken()}` };
    return this.http.post<any>(`${this.webApiServer}person`, person, { headers })
      .pipe(
        catchError(this.handleError)
      );
  }

  updatePerson(person: any): Observable<any> {
    const headers = { 'Authorization': `Bearer ${this.authService.getToken()}` };
    return this.http.put<any>(`${this.webApiServer}person`, person, { headers })
      .pipe(
        catchError(this.handleError)
      );
  }

  deletePerson(id: string): Observable<any> {
    const headers = { 'Authorization': `Bearer ${this.authService.getToken()}` };
    return this.http.delete<any>(`${this.webApiServer}person/${id}`, { headers })
      .pipe(
        catchError(this.handleError)
      );
  }

  addContact(personId: string, contact: any): Observable<any> {
    console.log('id', personId);
    const headers = { 'Authorization': `Bearer ${this.authService.getToken()}` };
    return this.http.post<any>(`${this.webApiServer}contact/${personId}`, contact, { headers })
      .pipe(
        catchError(this.handleError)
      );
  }

  getContactsByPersonId(personId: string): Observable<any[]> {
    const headers = { 'Authorization': `Bearer ${this.authService.getToken()}` };
    return this.http.get<any[]>(`${this.webApiServer}contact/person/${personId}`, { headers })
      .pipe(
        catchError(this.handleError)
      );
  }

  deleteContact(id: string): Observable<any> {
    const headers = { 'Authorization': `Bearer ${this.authService.getToken()}` };
    return this.http.delete<any>(`${this.webApiServer}contact/${id}`, { headers })
      .pipe(
        catchError(this.handleError)
      );
  }

  private handleError(error: HttpErrorResponse) {
    let errorMessage = 'Unknown error!';
    if (error.error instanceof ErrorEvent) {
      errorMessage = `Error: ${error.error.message}`;
    } else {
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    return throwError(errorMessage);
  }
}