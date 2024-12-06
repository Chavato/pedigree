import { Component } from '@angular/core';
import { HeaderComponent } from '../../components/header/header.component';
import { FooterComponent } from '../../components/footer/footer.component';
import { RouterLink } from '@angular/router';
import { CarouselComponent } from "../../components/carousel/carousel.component";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [HeaderComponent, FooterComponent, RouterLink, CarouselComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  carouselImages: string[] =
    ["assets/gallery/aereo-bowl-mute.jpg",
      "assets/gallery/backside-torque-corrimao-rua.jpg",
      "assets/gallery/pedigree-bowl.jpg",
      "assets/gallery/soul-corrimao-rua.jpg",
      "assets/gallery/topsoul-corrimao-rua.jpg"
    ]

}
