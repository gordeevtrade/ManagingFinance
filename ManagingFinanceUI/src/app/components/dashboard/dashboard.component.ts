
import { Component, ViewChild } from '@angular/core';
import { SubСategory } from '../../models/SubСategory';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { AddCategoryDialogComponent } from '../../dialog-components/category/add-category-dialog/add-category-dialog.component';
import { CategoryDataTransferService } from '../../services/data-transfer/category-data-transfer.service';
import { UpdateCategoryDialogComponent } from '../../dialog-components/category/update-category-dialog/update-category.component';
import { RemoveCategoryDialogComponent } from '../../dialog-components/category/remove-category-dialog/remove-category-dialog.component';
import { CategoryCollection } from 'src/app/models/CategoryCollection';
import { DashboardBuisinessLogicService } from 'src/app/services/business-logic/dashboard-buisiness-logic.service';
import { BudgetStatisticsDashBoardData } from 'src/app/models/BudgetStatisticsDashBoardData';
import { AddTransactionDialogComponent } from 'src/app/dialog-components/transaction/add-transaction-dialog/add-transaction-dialog.component';import {concatWith, first, Subject, Subscription, take, takeUntil} from 'rxjs';
import { ResultService } from 'src/app/services/result.service';
import { ProfileService } from 'src/app/services/profile/profile.service';
import { ShowTransactionsComponent } from 'src/app/dialog-components/transaction/show-transactions/show-transactions.component';
import {TransactionDataTransferService} from "../../services/data-transfer/transaction-data-transfer.service";
import { DialogService } from 'src/app/services/dialog.service';
import { StatisticsBusinessLogicServiceService } from 'src/app/services/business-logic/statistics-business-logic-service.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})

export class DashboardComponent {
  public categoryCollection? : CategoryCollection;
  public dashBoardData: BudgetStatisticsDashBoardData;
  public userEmail?: string;
  private _resultSubscription?: Subscription;

  constructor(
    private _dashboardBusinessLogicService: DashboardBuisinessLogicService,
    private _categoryDataTransfer: CategoryDataTransferService,
    private _statisticsData: StatisticsBusinessLogicServiceService,
    private _resultService:ResultService,
    private _profileService: ProfileService,
    private transactionDataTransfer: TransactionDataTransferService,
    private dialogService: DialogService,
    //public dialog: MatDialog,

    ) {
      this.dashBoardData = {
        totalBalance: undefined,
        transactionCount: undefined,
        incomeSubcategoriesCount: undefined,
        expenseSubcategoriesCount: undefined,
      };
    }

  ngOnInit(): void {
     this.loadData();
     this.updateAll();
     this.userEmail = this._profileService.getUserEmail();
}


  public updateAll() {
    this._statisticsData.getStatistics()
    .subscribe(data => {
      this.dashBoardData = {...data};
 }, error => {
      console.error('An error occurred:', error);
    });
  }

  public createTransaction(): void {
    const createTransactionDialogRef = this.dialogService.openDialog(AddTransactionDialogComponent, {
      width: '350px',
      height: '550px',
    });
    this.handleDialogClose(createTransactionDialogRef);
  }


 public showTransactions(subcategory: SubСategory): void{
   if (subcategory.id !== undefined) {
     this.transactionDataTransfer.setSubCategory(subcategory.id);
   }
  const showTransactionsDialogRef = this.dialogService.openDialog(ShowTransactionsComponent, {
    width: '650px',
    height: '550px',
    });
    this.handleDialogClose(showTransactionsDialogRef);
 }

  public addCategory(): void {
    const addSubcategoryDialogRef = this.dialogService.openDialog(AddCategoryDialogComponent, {
      width: '450px',
      height: '350px',
      });
      this.handleDialogClose(addSubcategoryDialogRef);
  }

  public removeSubcategory(subcategory: SubСategory): void {

      this._categoryDataTransfer.setSubcategory(subcategory);

    const deleteSubcategoryDialogRef = this.dialogService.openDialog(RemoveCategoryDialogComponent, {
      width: '550px',
      height: '250px',
      });
      this.handleDialogClose(deleteSubcategoryDialogRef);
}


 public updateSubCategory(subcategory: SubСategory): void {
    this._categoryDataTransfer.setSubcategory(subcategory);
    const updateSubcategoryDialogRef = this.dialogService.openDialog(UpdateCategoryDialogComponent, {
      width: '400px',
      height: '290px',
      });
      this.handleDialogClose(updateSubcategoryDialogRef);

  //  updateSubcategoryDialogRef.afterClosed().subscribe(() => {
  //    if (this._resultSubscription) {
  //      this._resultSubscription.unsubscribe();
  //    }
  //    this._resultSubscription = this._resultService.currentResult.subscribe(result => {
  //      if (result) {
  //        this.ngOnInit();
  //      }
  //    });
  //  });
  }

private loadData(): void {
  this._dashboardBusinessLogicService.returnCategoryCollection().subscribe((combinedData: CategoryCollection) => {
    this.categoryCollection = { ...combinedData };
  }, error => {
    console.error('An error occurred:', error);
  });
}

private handleDialogClose(dialogRef: MatDialogRef<any>): void {
  if (this._resultSubscription) {
    this._resultSubscription.unsubscribe();
  }
  dialogRef.afterClosed().subscribe(() => {
    this._resultSubscription = this._resultService.currentResult.subscribe(result => {
      if(result){
        this.ngOnInit();
      }
    });
  });
}


}





