import { Component } from '@angular/core';
import { HeaderComponent } from '../../components/header/header.component';
import { FooterComponent } from '../../components/footer/footer.component';
import { RouterLink } from '@angular/router';
import { CarouselComponent } from "../../components/carousel/carousel.component";
import { ImageInterface } from "../../models/ImageInterface";
import { TestimonyComponent } from "../../components/testimony/testimony.component";
import { TestimonyInterface } from '../../models/TestimonyInterface';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [HeaderComponent, FooterComponent, RouterLink, CarouselComponent, TestimonyComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  carouselImages: ImageInterface[] =
    [
      { url: "assets/gallery/aereo-bowl-mute.jpg", title: "aereo-bowl" },
      { url: "assets/gallery/pedigree-bowl.jpg", title: "bowl" },
      { url: "assets/gallery/backside-torque-corrimao-rua.jpg", title: "backside-torque" },
      { url: "assets/gallery/soul-corrimao-rua.jpg", title: "soul-corrimao" },
      { url: "assets/gallery/topsoul-corrimao-rua.jpg", title: "topsoul-corrimao-rua" }
    ]

  testimonyList: TestimonyInterface[] =
    [
      {
        author: "Ana Clara",
        text: "O professor Pedro me ajudou a superar o medo de patinar. Hoje, me sinto confiante e mais feliz!"
      },
      {
        author: "Lucas Mendes",
        text: "Aprender patins com o Pedigree foi incrível! Ele tem uma paciência e energia únicas."
      }
    ]
}
