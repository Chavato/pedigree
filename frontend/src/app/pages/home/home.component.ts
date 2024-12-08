import { Component } from '@angular/core';
import { HeaderComponent } from '../../components/header/header.component';
import { FooterComponent } from '../../components/footer/footer.component';
import { RouterLink } from '@angular/router';
import { CarouselComponent } from "../../components/carousel/carousel.component";
import { ImageInterface } from "../../models/ImageInterface";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [HeaderComponent, FooterComponent, RouterLink, CarouselComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  carouselImages: ImageInterface[] =
    [
      { url: "assets/gallery/aereo-bowl-mute.jpg", title: "aereo-bowl" },
      { url: "assets/gallery/backside-torque-corrimao-rua.jpg", title: "backside-torque" },
      { url: "assets/gallery/pedigree-bowl.jpg", title: "bowl" }
    ]

}
