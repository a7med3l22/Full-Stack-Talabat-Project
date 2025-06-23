import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { BreadcrumbService, Breadcrumb } from '../../core/services/breadcrumb';

@Component({standalone:false,
  selector: 'app-breadcrumb',
  templateUrl: './breadcrumb.html',
  styleUrls: ['./breadcrumb.scss']
})
export class BreadcrumbComponent implements OnInit {
  breadcrumbs$!: Observable<Breadcrumb[]>;  // خليها Observable

  constructor(private breadcrumbService: BreadcrumbService) { }

  ngOnInit(): void {
    // اشترك في الـ Observable بدل استدعاء دالة getBreadcrumbs
    this.breadcrumbs$ = this.breadcrumbService.breadcrumbs$;
  }
}
