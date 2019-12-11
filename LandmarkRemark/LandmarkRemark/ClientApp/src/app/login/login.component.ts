import { Component, OnInit, Inject, ViewChild, ElementRef } from '@angular/core';
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
  errorResponse: string;
  submitted:boolean=false;
  constructor(private route: ActivatedRoute, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private router: Router, private userService: UserService) { 

  }

  ngOnInit() {
    localStorage.removeItem('user');
    this.userService.setUser(null);
  }

  //user registers
  onRegister(){
    this.register = true;
  }

  //go back to login
  onCancel(){
    this.register = false;
  }

  //form submit for login/register
  onSubmit(g: NgForm){
if(!g.valid){
  return;
}
    this.submitted = true;
    const httpOptions = {

      headers: new HttpHeaders({

          'Content-Type': 'application/json'

      })
    };

    //register the user
    if(this.register){

    

        this.http.post<User>(this.baseUrl + 'api/User/register', this.user, httpOptions).subscribe(result => {

          this.submitted = false;
          //add the user details to local storage
          localStorage.setItem('user', JSON.stringify(result));
          this.userService.setUser(result);
          this.router.navigate(['/landmarkremark']);


      }, error =>  {this.submitted = false;
        this.user.username = "";
          this.user.password = "";
      console.log(error);
      this.errorResponse = error.error;});     //display the error for the user

     
    }
    //User log in
    else{
      this.http.post<User>(this.baseUrl + 'api/User/login', this.user, httpOptions).subscribe(result => {

        console.log(result);
        this.submitted = false;
        localStorage.setItem('user', JSON.stringify(result)); // add the user to local storage
        this.userService.setUser(result);
        this.router.navigate(['/landmarkremark']);

    }, error => {console.error(error)
      this.submitted = false;
      this.errorResponse = error.error;
    });    
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