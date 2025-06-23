import { Injectable } from '@angular/core';
import { Router, NavigationEnd, ActivatedRoute, Params } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';
import { filter } from 'rxjs/operators';

export interface Breadcrumb {
  label: string;
  url: string;
}

@Injectable({
  providedIn: 'root'
})
export class BreadcrumbService {
  private breadcrumbsSubject = new BehaviorSubject<Breadcrumb[]>([]);
  breadcrumbs$: Observable<Breadcrumb[]> = this.breadcrumbsSubject.asObservable();

  private breadcrumbs: Breadcrumb[] = [];

  constructor(private router: Router, private route: ActivatedRoute) {
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    ).subscribe(() => {
      const rootBreadcrumbs = this.createBreadcrumbs(this.route.root);
      const allBreadcrumbs = [{ label: 'Home', url: '/' }, ...rootBreadcrumbs];

      // إزالة التكرار المتتالي في الـ breadcrumbs
      const filteredBreadcrumbs = allBreadcrumbs.filter((item, index, array) => {
        if (index === 0) return true;
        return item.label !== array[index - 1].label;
      });

      this.breadcrumbs = filteredBreadcrumbs;
      this.breadcrumbsSubject.next(this.breadcrumbs);
    });
  }

  private createBreadcrumbs(route: ActivatedRoute, url: string = '', breadcrumbs: Breadcrumb[] = []): Breadcrumb[] {
    const children: ActivatedRoute[] = route.children;

    if (children.length === 0) {
      return breadcrumbs;
    }

    for (const child of children) {
      const routeURL: string = child.snapshot.url.map(segment => segment.path).join('/');
      if (routeURL !== '') {
        url += `/${routeURL}`;
      }

      const breadcrumbData = child.snapshot.data['breadcrumb'];

      // ✅ تخطي لو فيه skip: true
      if (breadcrumbData && typeof breadcrumbData === 'object' && breadcrumbData.skip) {
        this.createBreadcrumbs(child, url, breadcrumbs); // تابع بدون إضافة العنصر
        continue;
      }

      // ✅ استخراج label
      let label: string | null = null;
      if (typeof breadcrumbData === 'string') {
        label = breadcrumbData;
      } else if (typeof breadcrumbData === 'object' && breadcrumbData.label) {
        label = breadcrumbData.label;
      }

      // ✅ لو مفيش label نجيبه من الباراميتر
      if (!label && child.snapshot.params) {
        label = this.getLabelFromParams(child.snapshot.params);
      }

      if (label) {
        breadcrumbs.push({ label, url });
      }

      // ✅ تابع التكرار على أولاد هذا الـ route
      this.createBreadcrumbs(child, url, breadcrumbs);
    }

    return breadcrumbs;
  }

  private getLabelFromParams(params: Params): string | null {
    if (params['id']) {
      return `Order ${params['id']}`; // ممكن تغيرها حسب نوع الصفحة
    }
    return null;
  }

  // ✅ لتعديل label موجود أو إضافة جديد
  set(key: string, label: string): void {
    const index = this.breadcrumbs.findIndex(b => b.label === key || b.url === key);
    if (index !== -1) {
      this.breadcrumbs[index].label = label;
      this.breadcrumbsSubject.next([...this.breadcrumbs]);
    } else {
      this.breadcrumbs.push({ label, url: '' });
      this.breadcrumbsSubject.next([...this.breadcrumbs]);
    }
  }
}
