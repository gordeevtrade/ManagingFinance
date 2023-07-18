import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { SubСategory } from '../../../models/SubСategory';
import { Category } from '../../../models/Category';
import { CategoryBusinessLogicService } from 'src/app/services/business-logic/category-business-logic.service';
import {ResultService} from "../../../services/result.service";
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SubCategoryBusinessLogicService } from 'src/app/services/business-logic/sub-category-business-logic.service';
import { finalize } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-add-category-dialog',
  templateUrl: './add-category-dialog.component.html',
  styleUrls: ['./add-category-dialog.component.css']
})
export class AddCategoryDialogComponent {
 public categories: Category[] = [];
 public addCategoryForm: FormGroup;

  constructor(
    public dialogRef: MatDialogRef<AddCategoryDialogComponent>,
    private _categoryBusinessLogicService: CategoryBusinessLogicService,
    private _subCategoryBusinessLogicService: SubCategoryBusinessLogicService,
    private _resultService:ResultService,
    private _snackBar: MatSnackBar,
    private formBuilder: FormBuilder) 
    {
      this.addCategoryForm = this.formBuilder.group({
        name: ['', Validators.required],
        categoryTypeId: ['', Validators.required]
      });
    }

    ngOnInit(): void {
      this._categoryBusinessLogicService.getCategories()
        .subscribe((categories: Category[]) => {
          this.categories = categories;
        }, error => {
          console.log('Ошибка при получении списка категорий:', error);
        });
    }

   public addCategoryFormSubmit(): void {
   if (this.addCategoryForm.valid) {
    const { name, categoryTypeId } = this.addCategoryForm.value;
    const newSubCategory = new SubСategory();
    newSubCategory.name = name;
    newSubCategory.categoryTypeId = categoryTypeId;

    this._subCategoryBusinessLogicService.addSubcategory(newSubCategory).pipe(
      finalize(() => this.dialogRef.close())
    ).subscribe(
      () => {
        this._snackBar.open("Category Added Successfully", 'Close', {duration: 2000});
        this._resultService.changeResult(true);
      },
      (error) => {
        this._snackBar.open('Ошибка при получении списка категорий', 'Close', {duration: 2000});
        this._resultService.changeResult(false);
      }
    );
 }
   }
}
