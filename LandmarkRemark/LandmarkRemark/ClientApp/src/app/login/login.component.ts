import { Component, OnInit, Inject } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../services/user-service.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  user: User={ id:0,
    username: "",
    password: "",
    email: "", token:""};
  register: boolean = false;
  constructor(private route: ActivatedRoute, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private router: Router, private userService: UserService) { 

  }

  ngOnInit() {
    localStorage.removeItem('user');
    this.userService.setUser(null);
  }

  onRegister(){
    this.register = true;
  }

  onCancel(){
    this.register = false;
  }

  onSubmit(g: NgForm){
if(!g.valid){
  return;
}
    const httpOptions = {

      headers: new HttpHeaders({

          'Content-Type': 'application/json'

      })
    };

    if(this.register){

    

        this.http.post<User>(this.baseUrl + 'api/User/register', this.user, httpOptions).subscribe(result => {

          console.log(result);
          localStorage.setItem('user', JSON.stringify(result));
          this.userService.setUser(result);
          this.router.navigate(['/landmarkremark']);


      }, error => console.error(error));    

      this.register = false;
      this.user.username = "";
      this.user.password = "";
    }
    else{
      this.http.post<User>(this.baseUrl + 'api/User/login', this.user, httpOptions).subscribe(result => {

        console.log(result);
        localStorage.setItem('user', JSON.stringify(result));
        this.userService.setUser(result);
        this.router.navigate(['/landmarkremark']);

    }, error => console.error(error));    
    }
  }
}

export interface User {
  id: number,
  username: string,
  password: string,
  email: string,
  token: string
}