import { Component, Input, OnDestroy, AfterRenderPhase, afterNextRender, NgZone } from '@angular/core';
import { NgFor } from '@angular/common';
import { ImageInterface } from '../../models/ImageInterface';

@Component({
  selector: 'app-carousel',
  standalone: true,
  imports: [NgFor],
  templateUrl: './carousel.component.html',
  styleUrl: './carousel.component.scss'
})
export class CarouselComponent implements OnDestroy {
  @Input() images: ImageInterface[] = [];
  currentSlide: number = 0;

  private timer: any;

  constructor(ngZone: NgZone) {
    this.startAutoSlide(ngZone);
  }

  ngOnDestroy() {
    clearInterval(this.timer); // Limpa o timer ao destruir o componente
  }

  startAutoSlide(ngZone: NgZone) {
    afterNextRender(() => {
      ngZone.run(() => {
        this.timer = setInterval(() => {
          this.nextSlide();
        }, 5000); // Troca a cada 5 
      });
    }, { phase: AfterRenderPhase.Write });
  }

  goToSlide(index: number): void {
    this.currentSlide = index;
  }

  nextSlide(): void {
    this.currentSlide = (this.currentSlide + 1) % this.images.length;
  }

  prevSlide(): void {
    this.currentSlide = (this.currentSlide - 1 + this.images.length) % this.images.length;
  }
}


