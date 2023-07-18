import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { Category } from 'src/app/models/Category';
import { SubСategory } from 'src/app/models/SubСategory';
import { Transaction } from 'src/app/models/Transaction';
import { SubCategoryApiService } from 'src/app/services/api/sub-category-api.service';
import { TransactionBusinessLogicService } from 'src/app/services/business-logic/transaction-business-logic.service';
import {ResultService} from "../../../services/result.service";
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {CategoryBusinessLogicService} from "../../../services/business-logic/category-business-logic.service";
import { finalize } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-add-transaction-dialog',
  templateUrl: './add-transaction-dialog.component.html',
  styleUrls: ['./add-transaction-dialog.component.css']
})
export class AddTransactionDialogComponent {
  public transactionForm: FormGroup;
   public selectedCategory?: Category;
   public categories: Category[] = [];
   public subCategories: SubСategory[] = [];
   public expenseCategoryIds: number[] = [2];


constructor(
  public dialogRef: MatDialogRef<AddTransactionDialogComponent>,
  private _transactionBusinessLogicService: TransactionBusinessLogicService,
  private _categoryBusinessLogicService: CategoryBusinessLogicService,
  private _categoryApiService:SubCategoryApiService,
  private _resultService:ResultService,
  private _snackBar: MatSnackBar,
  private formBuilder: FormBuilder
) {

  this.transactionForm = this.formBuilder.group({
    categoryId: [null, Validators.required],
    subcategoryId: [null, Validators.required],
    date: [null, Validators.required],
    note: [''],
    amount: [null, Validators.required]
  });
}

ngOnInit() {
  this._categoryBusinessLogicService.getCategories().subscribe(
    (categories: Category[]) => {
      this.categories = categories;
    },
    (error) => {
      console.log('Ошибка при получении категорий:', error);
    }
  );
  this.transactionForm.get('categoryId')?.valueChanges.subscribe((selectedCategoryId) => {
    this.returnSubCategoryOnCategorySelect(selectedCategoryId);
  });
}


public returnSubCategoryOnCategorySelect(selectedCategoryId:number) {
  if (selectedCategoryId) {
    this._categoryApiService.getSubCategoriesByCategoryID(selectedCategoryId).subscribe(
      (subcategories: SubСategory[]) => {
        this.subCategories = subcategories;
      },
      (error) => {
        console.log('Ошибка при получении подкатегорий:', error);
      }
    );
  } else {
    this.subCategories = [];
  }
}

public createNewTransaction(): void {
  const { subcategoryId, date, note, amount,categoryId } = this.transactionForm.value;
  const newTransaction = new Transaction();
  newTransaction.categoryId = subcategoryId;
  newTransaction.date = date;
  newTransaction.note = note;
  newTransaction.amount = amount;
  
  if (this.expenseCategoryIds.includes(+categoryId)) {
    newTransaction.amount = -amount;
  }

  this._transactionBusinessLogicService.addTransaction(newTransaction)
    .pipe(
      finalize(() => {
        this.dialogRef.close();
      })
    )
    .subscribe(
      () => {
        this._snackBar.open("Транзакция успешно добавлена!", 'Close', {duration: 2000});
        this._resultService.changeResult(true);
      },
      (error) => {
        this._snackBar.open("Ошибка при добавлении транзакции!", 'Close', {duration: 2000});
      }
    );
}
}

