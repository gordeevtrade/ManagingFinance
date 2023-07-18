import { Injectable } from '@angular/core';
import { SubCategoryApiService } from '../api/sub-category-api.service';
import { Category } from 'src/app/models/Category';
import { SubСategory } from 'src/app/models/SubСategory';
import { Observable, catchError, tap, throwError } from 'rxjs';
import {CategoryApiService} from "../api/category-api.service";

@Injectable({
  providedIn: 'root',
})
export class CategoryBusinessLogicService {
  constructor(
    private categoryApiService: CategoryApiService,
  ) {}

  
 public getCategories(): Observable<Category[]> {
    return this.categoryApiService.getCategory();
  }

  // public addSubcategory(subcategory: SubСategory): Observable<any> {
  //   return this.subCategoryApiService.addSubcategory(subcategory);
  // }

  // public deleteSubcategory(subcategoryId: number): Observable<any> {
  //   return this.subCategoryApiService.deleteSubcategory(subcategoryId);
  // }

  // public updateSubcategory(subcategory: SubСategory): Observable<any> {
  //   return this.subCategoryApiService.updateSubcategory(subcategory);
  // }





//  public loadData() {
//     return forkJoin({
//       categories: this.categoryApiService.getCategory(),
//       subcategories: this.categoryApiService.getAllSubCategories(),
//       transactions: this.transactionApiService.getTransactions(),
//     }).pipe(
//       map(({ categories, subcategories, transactions }) => {
//         subcategories?.forEach(subcategory => {
//           subcategory.transactionsCount = transactions?.filter(
//             (t) => t.categoryId === subcategory.id
//           ).length;
//         });

//         return { categories, subcategories, transactions };
//       })
//     );
//   }

}
