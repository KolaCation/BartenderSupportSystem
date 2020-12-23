import { Component, OnInit } from '@angular/core';
import { AuthorizeService } from 'src/api-authorization/authorize.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  isAuthenticated = false;

  constructor(private _authorizeService: AuthorizeService) {}

  ngOnInit(): void {
    this._authorizeService
      .isAuthenticated()
      .subscribe((isAuthenticated) => (this.isAuthenticated = isAuthenticated));
  }
}
