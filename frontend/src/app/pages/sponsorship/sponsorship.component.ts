import { Component } from '@angular/core';
import { HeaderComponent } from "../../components/header/header.component";
import { FooterComponent } from "../../components/footer/footer.component";

@Component({
  selector: 'app-sponsorship',
  standalone: true,
  imports: [HeaderComponent, FooterComponent],
  templateUrl: './sponsorship.component.html',
  styleUrl: './sponsorship.component.scss'
})
export class SponsorshipComponent {

}
