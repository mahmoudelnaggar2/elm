import { Component } from '@angular/core';


@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrl: './books.component.css'
})
export class BooksComponent {
  searchInput: string = "";
  searchKey: string = "";

  searchForBook(){
    console.log('clicked')
    this.searchKey = this.searchInput;
  }
}
