import { Component } from '@angular/core';
import { ReportBusinessLogicService } from 'src/app/services/business-logic/report-business-logic.service';
import { TransactionDataTransferService } from 'src/app/services/data-transfer/transaction-data-transfer.service';
import { Transaction } from 'src/app/models/Transaction';
import { MatDialogRef } from '@angular/material/dialog';
import {ResultService} from "../../../services/result.service";
import { TransactionBusinessLogicService } from 'src/app/services/business-logic/transaction-business-logic.service';
import { finalize } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-remove-transaction-dialog',
  templateUrl: './remove-transaction-dialog.component.html',
  styleUrls: ['./remove-transaction-dialog.component.css']
})
export class RemoveTransactionDialogComponent {

  private _transaction? : Transaction;
  constructor(
    private _transactionBusinessLogicService: TransactionBusinessLogicService,
    private _transactionDataTransfer: TransactionDataTransferService,
    public dialogRef: MatDialogRef<RemoveTransactionDialogComponent>,
    private _snackBar: MatSnackBar,
    private _resultService:ResultService
  ) { }

  ngOnInit(): void {
    this._transaction = this._transactionDataTransfer.getTransaction();
  }

 
   public removeTransaction(): void {
    const removedTransaction: Transaction = {...this._transaction};
    if (removedTransaction !== undefined) {
      this._transactionBusinessLogicService.removeTransaction(removedTransaction).pipe(
        finalize(() => this.dialogRef.close())
      ).subscribe(
        () => {
          this._snackBar.open("Transaction Remove Successfully", 'Close', {duration: 2000});
          this._resultService.changeResult(true);
        },
        (error) => {
          this._snackBar.open("Eror", 'Close', {duration: 2000});
          this._resultService.changeResult(false);
        }
      );
    }
  }
}
