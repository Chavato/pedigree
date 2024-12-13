import { Component } from '@angular/core';
import { HeaderComponent } from "../../components/header/header.component";
import { FooterComponent } from "../../components/footer/footer.component";
import { CarouselComponent } from "../../components/carousel/carousel.component";
import { ImageInterface } from '../../models/ImageInterface';
import { TestimonyInterface } from '../../models/TestimonyInterface';
import { TestimonyComponent } from "../../components/testimony/testimony.component";

@Component({
  selector: 'app-classes',
  standalone: true,
  imports: [HeaderComponent, FooterComponent, CarouselComponent, TestimonyComponent],
  templateUrl: './classes.component.html',
  styleUrl: './classes.component.scss'
})
export class ClassesComponent {

  carouselImages: ImageInterface[] = []

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
