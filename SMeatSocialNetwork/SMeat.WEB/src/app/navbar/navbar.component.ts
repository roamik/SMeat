import { Component, OnInit, ViewChild, HostListener } from '@angular/core';
import { AuthGuard } from '../_guards/auth.guard';
import { User } from "../_models/user";

import { MatSidenav } from '@angular/material/sidenav';
import { LangChangeEvent } from "@ngx-translate/core";
import { TranslateService } from '@ngx-translate/core';
import { NgSelectModule } from '@ng-select/ng-select';
import { LanguagesEnum, LanguagesIconEnum } from "../_enums/languages";
import { EnumToArrayHelper } from "../_helpers/EnumToArrayHelper";
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  @ViewChild('sidenav') sidenav: any;

  constructor(private guard: AuthGuard,
    private translate: TranslateService,
    private enumSelector: EnumToArrayHelper,
    private _router: Router,
    private _route: ActivatedRoute
  ) { }

  selectedLang: any;
  value: any;
  searchByWord: string;


  public languages: any = this.enumSelector.enumSelector(LanguagesEnum, LanguagesIconEnum);

  get isAuthenticated(): boolean { return this.guard.isAuthenticated }

  get userId(): string { return this.guard.userId; }

  ngOnInit() {
    this.selectedLang = this.languages[0].value;
    this.switchLanguage();
  }

  switchLanguage() {
    var lang = LanguagesEnum[this.selectedLang];
    this.translate.use(lang);
  }

}
