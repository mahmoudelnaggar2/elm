import { Component, Input } from '@angular/core';
import { Book } from '../../../Models/book';

@Component({
  selector: 'app-book-detail',
  templateUrl: './book-detail.component.html',
  styleUrl: './book-detail.component.css'
})
export class BookDetailComponent {

  @Input() book?: Book;
  
}
