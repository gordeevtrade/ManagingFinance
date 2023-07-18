import { Injectable } from '@angular/core';
import { Transaction } from '../../models/Transaction';

@Injectable({
  providedIn: 'root'
})
export class TransactionDataTransferService {
  private transactions: Transaction | undefined;
  private  subCategoryId?:number;
  constructor() { }

  public setTransaction(transaction: Transaction): void {
    this.transactions = { ...transaction };

  }
  public getTransaction(): Transaction | undefined {
    return this.transactions;
  }

  public  setSubCategory(id : number){
    this.subCategoryId = id;
  }

  public  getSubCategoryId(){
    return this.subCategoryId;
  }

}
