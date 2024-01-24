import { Component, Input, HostListener} from '@angular/core';
import { Book } from '../../../Models/book';
import { BookService } from '../../../Services/book.service';
import { MessageService } from '../../../Services/message.service';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrl: './book-list.component.css'
})
export class BookListComponent {

  books: Book[] = [];  
  isLoading: boolean = true;
  loadingMessage: string = "Loading...";
  @Input() searchKey: string = "";
  pageNumber:number = 1;
  oldSearchKey: string = "";

  constructor(private bookService: BookService, 
    private messageService: MessageService){}

  bookSearch():void{

      this.isLoading = true;
     
      this.bookService.searchBooks(this.searchKey!, this.pageNumber, 10)
      .subscribe((result) => {        
        
        if(result.length > 0){

          if(this.searchKey != this.oldSearchKey)
              this.books = [];

         this.books = [...this.books, ...result];
         this.isLoading = false;

         this.oldSearchKey = this.searchKey;
         this.pageNumber++;
        }else{
          this.loadingMessage = "No Data Found";
        }
        
      },
      (error) => {
        this.loadingMessage = error;
      })    
  }

  ngOnInit(){
    this.bookSearch();   
  }

  ngOnChanges(){    
    this.bookSearch();
  }

  @HostListener('window:scroll', ['$event'])
  onWindowScroll() {
     if ((window.innerHeight + window.scrollY) >= document.body.offsetHeight) {
      this.bookSearch();
     }
}
}
