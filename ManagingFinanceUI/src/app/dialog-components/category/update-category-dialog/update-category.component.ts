import { Component } from '@angular/core';
import { SubСategory } from '../../../models/SubСategory';
import { MatDialogRef } from '@angular/material/dialog';
import { CategoryDataTransferService } from '../../../services/data-transfer/category-data-transfer.service';
import { ResultService } from 'src/app/services/result.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SubCategoryBusinessLogicService } from 'src/app/services/business-logic/sub-category-business-logic.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-update-category',
  templateUrl: './update-category.component.html',
  styleUrls: ['./update-category.component.css']
})
export class UpdateCategoryDialogComponent {
  public subcategorie?: SubСategory| undefined;
  public editCategoryForm: FormGroup;

  constructor(
    private _dialogRef: MatDialogRef<UpdateCategoryDialogComponent>,
    private _categoryDataTransfer:CategoryDataTransferService,
    private _subCategoryBusinessLogicService: SubCategoryBusinessLogicService,
    private _resultService:ResultService,
    private formBuilder: FormBuilder,
    private _snackBar: MatSnackBar
  ) {

    this.editCategoryForm = this.formBuilder.group({
      name: ['', Validators.required]
    });
  }

  ngOnInit():void{
    this.subcategorie = this._categoryDataTransfer.getSubcategory();
    if (this.subcategorie) {
      this.editCategoryForm.patchValue({
        name: this.subcategorie.name
      });
    }
  }

  // public updateCategory(): void {
  //   if (this.editCategoryForm.valid) {
  //   if (this.subcategorie) {
  //     this.subcategorie.name  = this.editCategoryForm.value.name;
  //     this._subCategoryBusinessLogicService.updateSubcategory(this.subcategorie).subscribe(
  //       () => {
  //         alert('Подкатегория успешно обновлена');
  //         this._resultService.changeResult(true);
  //         this._dialogRef.close();
  //       },
  //       () => {
  //         alert('Ошибка при обновлении подкатегории');
  //         this._resultService.changeResult(false);
  //         this._dialogRef.close();
  //       }
  //     );
  //   }
  // }

  public updateCategory(): void {
    if (this.editCategoryForm.valid && this.subcategorie) {
      const updatedSubCategory: SubСategory = {...this.subcategorie};
      const { name } = this.editCategoryForm.value;
      updatedSubCategory.name = name;

      this._subCategoryBusinessLogicService.updateSubcategory(updatedSubCategory)
      .pipe(
        finalize(() => this._dialogRef.close()) 
      )
      .subscribe(
        () => {
          this._snackBar.open('Подкатегория успешно обновлена', 'Close', {duration: 2000});
          this._resultService.changeResult(true);
        },
        () => {
          this._snackBar.open('Ошибка при обновлении подкатегории', 'Close', {duration: 2000});
          this._resultService.changeResult(false);
        }
      );
    }

  }

}



