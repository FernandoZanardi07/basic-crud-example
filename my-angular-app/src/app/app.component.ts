import { Component } from '@angular/core';
import { MyService } from './services/my-service.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})


export class AppComponent {
  constructor(private myService: MyService) { }
  title = 'my-angular-app';
  isLogin: boolean = true;
  userLogin: any = { Username: '', Password: '' };
  errorMessage: string = '';

  ngOnInit(): void {
    this.isLogin = !this.myService.getAuthStatus();
  }

  login(): void {
    console.log('Login:', this.userLogin);
    this.myService.getAuth(this.userLogin).subscribe(
      (data: any) => {
        console.log('Auth:', data);
        this.isLogin = !data.auth;

        if (!data.auth)
          this.errorMessage = 'Username or password is incorrect';
      },
      (error) => {
        console.error('Error fetching auth', error);
        this.errorMessage = 'Error while trying to login';
      }
    );
  }

  logout(): void {
    this.myService.logout();
    this.isLogin = true;
  }

}