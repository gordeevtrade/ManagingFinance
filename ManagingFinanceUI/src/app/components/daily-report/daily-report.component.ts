import { Component } from '@angular/core';
import { PeriodReport } from '../../models/PeriodReport';
import { SubСategory } from '../../models/SubСategory';
import { Transaction } from '../../models/Transaction';
import { UpdateTransactionDialogComponent } from '../../dialog-components/transaction/update-transaction-dialog/update-transaction-dialog.component';
import { TransactionDataTransferService } from '../../services/data-transfer/transaction-data-transfer.service';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ReportBusinessLogicService } from 'src/app/services/business-logic/report-business-logic.service';
import { RemoveTransactionDialogComponent } from 'src/app/dialog-components/transaction/remove-transaction-dialog/remove-transaction-dialog.component';
import {ResultService} from "../../services/result.service";
import {Subscription} from "rxjs";
import { DialogService } from 'src/app/services/dialog.service';

@Component({
  selector: 'app-daily-report',
  templateUrl: './daily-report.component.html',
  styleUrls: ['./daily-report.component.css']
})
export class DailyReportComponent {

  public periodReport?: PeriodReport;
  public transactionList?: Transaction[];
  public startDate: string = '';
  public endDate: string = '';
  private _resultSubscription?: Subscription;


  constructor(
    private reportBusinessLogicService: ReportBusinessLogicService,
    private transactionDataTransfer: TransactionDataTransferService,
  //  public dialog: MatDialog,
    private dialogService: DialogService,
    private resultService:ResultService
  ) { }

  ngOnInit(): void {
  }

  public getReport(): void {
    this.reportBusinessLogicService.getReport(this.startDate, this.endDate).subscribe(
      (report) => {
        this.periodReport = report;
        this.transactionList = report.transactions;
      },
      (error) => {
        alert("Нет данных");
      }
    );
  }

  public removeTransaction(transaction: Transaction): void {
   this.transactionDataTransfer.setTransaction(transaction);
   const removeTransactiondialogRef = this.dialogService.openDialog(RemoveTransactionDialogComponent, {
    width: '550px',
    height: '250px',
    });
    this.handleDialogClose(removeTransactiondialogRef);
  }

  public updateTransactionName(data: Transaction): void {
    this.transactionDataTransfer.setTransaction(data);
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
    this._resultSubscription = this.resultService.currentResult.subscribe(result => {
      if(result){
        this.getReport();
      }
    });
  });
}


}

