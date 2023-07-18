import { Component } from '@angular/core';
import {TransactionDataTransferService} from "../../../services/data-transfer/transaction-data-transfer.service";
import {MatDialog, MatDialogRef} from "@angular/material/dialog";
import { TransactionApiService } from 'src/app/services/api/transaction-api.service';
import { Transaction } from 'src/app/models/Transaction';
import { UpdateTransactionDialogComponent } from '../update-transaction-dialog/update-transaction-dialog.component';
import { RemoveTransactionDialogComponent } from '../remove-transaction-dialog/remove-transaction-dialog.component';
import { Subscription } from 'rxjs';
import { DialogService } from 'src/app/services/dialog.service';

@Component({
  selector: 'app-show-transactions',
  templateUrl: './show-transactions.component.html',
  styleUrls: ['./show-transactions.component.css']
})
export class ShowTransactionsComponent {

  public transactions?: Transaction[];
  private _resultSubscription?: Subscription;

  constructor(
    private _transactionDataTransfer:TransactionDataTransferService,
    private transactionService: TransactionApiService,
   //  public dialog: MatDialog,
    private dialogService: DialogService,
    public dialogRef: MatDialogRef<ShowTransactionsComponent>
  ) {}

  ngOnInit():void{
    const subCategoryId = this._transactionDataTransfer.getSubCategoryId();
    if(subCategoryId)
    this.loadTransactionsByCategory(subCategoryId);
  }

  public loadTransactionsByCategory(subCategoryId:number ) {
    this.transactionService.getTransactionsByCategory(subCategoryId)
      .subscribe(transactions => this.transactions = transactions);
  }


  public removeTransaction(transaction: Transaction): void {
    this._transactionDataTransfer.setTransaction(transaction);
    const removeTransactiondialogRef = this.dialogService.openDialog(RemoveTransactionDialogComponent, {
      width: '550px',
      height: '250px',
      });
     this.handleDialogClose(removeTransactiondialogRef);
   }

   public updateTransactionName(transaction: Transaction): void {
     this._transactionDataTransfer.setTransaction(transaction);
     const updateTransactionNameDialogRef = this.dialogService.openDialog(UpdateTransactionDialogComponent, {
      width: '450px',
      height: '390px',
      });
    this.handleDialogClose(updateTransactionNameDialogRef);
   }


private handleDialogClose(dialogRef: MatDialogRef<any>): void {
  if (this._resultSubscription) {
    this._resultSubscription.unsubscribe();
  }
  dialogRef.afterClosed().subscribe(() => {
       this.dialogRef.close();
  });
}

}
