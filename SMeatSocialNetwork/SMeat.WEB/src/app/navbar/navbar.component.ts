import { Component, OnInit, ViewChild, HostListener } from '@angular/core';
import { AuthGuard } from '../_guards/auth.guard';
import { User } from "../_models/user";

import { MatSidenav } from '@angular/material/sidenav';

@Component({
  selector: 'navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  @ViewChild('sidenav') sidenav: any;

  constructor(private guard: AuthGuard) {}

  get isAuthenticated(): boolean { return this.guard.isAuthenticated }

  get userId(): string { return this.guard.userId; }

  ngOnInit() {

  }
  
}
