import { Injectable } from '@angular/core';

@Injectable()
export class UserService {

  constructor() { }
  private _user: any;
  public getUser(): any {
    return this._user;
  }
  public setUser(value: any) {
    this._user = value;
  }
}