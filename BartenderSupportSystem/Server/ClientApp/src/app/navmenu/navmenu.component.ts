import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthorizeService } from 'src/api-authorization/authorize.service';

@Component({
  selector: 'app-navmenu',
  templateUrl: './navmenu.component.html',
  styleUrls: ['./navmenu.component.css'],
})
export class NavmenuComponent implements OnInit {
  isAuthenticated = false;
  isAdmin = false;

  constructor(
    private _authorizeService: AuthorizeService,
    private _router: Router
  ) {}

  ngOnInit(): void {
    this._authorizeService.isAuthenticated().subscribe((isAuthenticated) => {
      if (!isAuthenticated) {
        this._router.navigate(['/authentication/login']);
      } else {
        this.isAuthenticated = true;
        this._authorizeService
          .getUserRole()
          .subscribe((role) => (this.isAdmin = role.toLowerCase() === 'admin'));
      }
    });
  }
}
