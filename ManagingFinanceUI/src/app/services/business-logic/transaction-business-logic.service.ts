
import { Injectable } from '@angular/core';
import {Observable, map, EMPTY} from 'rxjs';
import { TransactionApiService } from '../api/transaction-api.service';
import { Transaction } from 'src/app/models/Transaction';

@Injectable({
  providedIn: 'root'
})
export class TransactionBusinessLogicService {
  constructor(
    private _transactionApiService: TransactionApiService,
  ) {}

  public addTransaction(transactionData: Transaction): Observable<Transaction> {
    return this._transactionApiService.addTransaction(transactionData);
  }

  public removeTransaction(transaction: Transaction) {
    if (!transaction.id) {
      return EMPTY;
    }
    return this._transactionApiService.deleteTransaction(transaction.id);
  }

  public updateTransaction(transaction: Transaction): Observable<Transaction> {
    return this._transactionApiService.updateTransaction(transaction);
  }

}
