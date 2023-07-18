import { Component } from '@angular/core';
import { Transaction } from '../../../models/Transaction';
import { MatDialogRef } from '@angular/material/dialog';
import { TransactionDataTransferService } from '../../../services/data-transfer/transaction-data-transfer.service';
import { TransactionBusinessLogicService } from 'src/app/services/business-logic/transaction-business-logic.service';
import {ResultService} from "../../../services/result.service";
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-update-transaction-dialog',
  templateUrl: './update-transaction-dialog.component.html',
  styleUrls: ['./update-transaction-dialog.component.css']
})
export class UpdateTransactionDialogComponent {
  public transactionForm: FormGroup;
  public transationForUpdate?: Transaction| undefined;

  constructor(
    private _transactionDataTransfer:TransactionDataTransferService,
    private _transactionBusinessLogicService: TransactionBusinessLogicService,
    private _resultService:ResultService,
    public dialogRef: MatDialogRef<UpdateTransactionDialogComponent>,
    private formBuilder: FormBuilder,
    private _snackBar: MatSnackBar,
    ) 
    
    {
      this.transactionForm = this.formBuilder.group({
        amount: ['', Validators.required],
        note: [''],
        date: [null, Validators.required]
      });
    }

  // ngOnInit():void{
  //   this.transationForUpdate = this._transactionDataTransfer.getTransaction();
  // }

  ngOnInit(): void {
    this.transationForUpdate = this._transactionDataTransfer.getTransaction();
    if (this.transationForUpdate) {
      this.transactionForm.patchValue({
        amount: this.transationForUpdate.amount,
        note: this.transationForUpdate.note,
        date: this.transationForUpdate.date
      });
    }
  }


  public updateTransaction() {
    if (this.transactionForm.valid && this.transationForUpdate) {
      const updatedTransaction: Transaction = {...this.transationForUpdate};
      updatedTransaction.amount = this.transactionForm.value.amount;
      updatedTransaction.note = this.transactionForm.value.note;
      updatedTransaction.date = this.transactionForm.value.date;
      this._transactionBusinessLogicService.updateTransaction(updatedTransaction).subscribe(
        () => {
          this._snackBar.open("Транзакция успешно обновлена!", 'Close', {duration: 2000});
          this._resultService.changeResult(true);
          this.dialogRef.close();
        },
        (error) => {
          this._snackBar.open("Ошибка при обновлении транзакции!", 'Close', {duration: 2000});
        }
      );
    }
  }




  // public updateTransaction() {
  //   if (this.transationForUpdate) {
  //     this._transactionBusinessLogicService.updateTransaction(this.transationForUpdate).subscribe(
  //       () => {
  //         alert('Транзакция успешно обновлена');
  //         this._resultService.changeResult(true);
  //         this.dialogRef.close();
  //       },
  //       (error) => {
  //         alert('Ошибка при обновлении транзакции');
  //       }
  //     );
  //   }
  // }

}
