import { Component } from '@angular/core';
import { NavigationEnd, Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  template: '<router-outlet/>'
})
export class AppComponent {
  constructor(private router: Router) { }

  ngOnInit(): void {
    if (typeof window !== 'undefined') {
      this.router.events.subscribe(event => {
        if (event instanceof NavigationEnd) {
          window.scrollTo(0, 0);
        }
      });
    }
  }

}
