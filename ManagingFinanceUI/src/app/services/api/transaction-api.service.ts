import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Transaction } from '../../models/Transaction';
import { Observable, map } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TransactionApiService {
  
  private transactionUrl = `${environment.apiUrl}/Transaction`;

  constructor(private http: HttpClient) {}

 public updateTransaction(updatedTransaction: Transaction): Observable<Transaction> {
    const url = `${this.transactionUrl}`;
    return this.http.put(url, updatedTransaction);
  }
public  deleteTransaction(id: number): Observable<void> {
    const url = `${this.transactionUrl}/${id}`;
    return this.http.delete<void>(url);
  }

public  addTransaction(updatedTransaction: Transaction): Observable<Transaction> {
    const url = `${this.transactionUrl}`;
    return this.http.post(url, updatedTransaction);
  }

public  getTransactions(): Observable<Transaction[]> {
    return this.http.get<Transaction[]>(`${this.transactionUrl}`);
  }

  public getTransactionsByCategory(categoryId: number): Observable<Transaction[]> {
    const url = `${this.transactionUrl}/category/${categoryId}`;
    return this.http.get<Transaction[]>(url);
  }
}
