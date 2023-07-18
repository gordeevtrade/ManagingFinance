import { Injectable } from '@angular/core';
import { CategoryCollection } from 'src/app/models/CategoryCollection';
import { catchError, tap, throwError } from 'rxjs';
import { CategoryApiService } from '../api/category-api.service';

@Injectable({
  providedIn: 'root'
})
export class DashboardBuisinessLogicService {
  private categoryCollection? : CategoryCollection;
  constructor( private categoryApiService:CategoryApiService) { }

  public returnCategoryCollection() {
    return this.categoryApiService.getCategoryCollection().pipe(
      tap((categoryCollection: CategoryCollection) => {
        this.categoryCollection= categoryCollection;
      }),
      catchError((error) => {
        console.error('An error occurred:', error);
        return throwError(error);
      })
    );
  }


}
