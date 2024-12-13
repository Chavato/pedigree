import { Component, Input, input } from '@angular/core';
import { TestimonyInterface } from '../../models/TestimonyInterface';

@Component({
  selector: 'app-testimony',
  standalone: true,
  imports: [],
  templateUrl: './testimony.component.html',
  styleUrl: './testimony.component.scss'
})
export class TestimonyComponent {
  @Input() testimonyList: TestimonyInterface[] = []
}
