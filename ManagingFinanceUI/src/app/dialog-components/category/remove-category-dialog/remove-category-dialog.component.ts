import { Component } from '@angular/core';
import { SubСategory } from '../../../models/SubСategory';
import { MatDialogRef } from '@angular/material/dialog';
import { CategoryDataTransferService } from '../../../services/data-transfer/category-data-transfer.service';
import { ResultService } from 'src/app/services/result.service';
import { SubCategoryBusinessLogicService } from 'src/app/services/business-logic/sub-category-business-logic.service';
import { finalize } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-remove-category-dialog',
  templateUrl: './remove-category-dialog.component.html',
  styleUrls: ['./remove-category-dialog.component.css']
})
export class RemoveCategoryDialogComponent {
  public subCategory?: SubСategory;
  constructor(
    public dialogRef: MatDialogRef<RemoveCategoryDialogComponent>,
    private _categoryDataTransfer:CategoryDataTransferService,
    private _subCategoryBusinessLogicService: SubCategoryBusinessLogicService,
    private _snackBar: MatSnackBar,
    private _resultService:ResultService
    )
    { }

  ngOnInit():void{
    this.subCategory = this._categoryDataTransfer.getSubcategory();
  }

public removeSubCategory(): void {
  const removedSubCategory: SubСategory = {...this.subCategory};
  const subCategoryId = removedSubCategory.id; 
  if (subCategoryId !== undefined) {
    this._subCategoryBusinessLogicService.deleteSubcategory(subCategoryId).pipe(
      finalize(() => this.dialogRef.close())
    ).subscribe(
      () => {
        this._snackBar.open("Category Remove Successfully", 'Close', {duration: 2000});
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
