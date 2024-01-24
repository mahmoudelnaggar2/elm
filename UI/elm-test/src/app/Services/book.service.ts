import { Injectable } from '@angular/core';
import { Book } from '../Models/book';
import { Observable, of } from 'rxjs';
import { MessageService } from './message.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class BookService {

  private booksUrl = 'https://localhost:44332/api';

  constructor(private http: HttpClient,
    private messageService: MessageService) { }

  getBooks(): Observable<Book[]> {
    return this.http.get<Book[]>(`${this.booksUrl}/books`)
    .pipe(
      tap(_ => this.log('fetched books')),
      catchError(this.handleError<Book[]>('getBooks', []))
    );
  }

searchBooks(searchKey: string, pageNumber: number, pageSize: number): Observable<Book[]> {
  return this.http.get<Book[]>(`${this.booksUrl}/books?searchKey=${searchKey}`).pipe(
    tap(x => x.length ?
       this.log(`found book matching "${searchKey}"`) :
       this.log(`no books matching "${searchKey}"`)),
    catchError(this.handleError<Book[]>('searchBooks', []))
  );
}


private log(message: string) {
  this.messageService.add(`BookService: ${message}`);
}

private handleError<T>(operation = 'operation', result?: T) {
  return (error: any): Observable<T> => {
      
    console.error(error);

    this.log(`${operation} failed: ${error.message}`);

    return of(result as T);
  };
}
};

