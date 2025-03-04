import { Component } from '@angular/core';
import { UserService } from '../services/user-service.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  constructor(private userService: UserService){}

  collapse() {
    this.isExpanded = false;
  }

  showLogout(){
    return this.userService.getUser() || localStorage.getItem('user');
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
