import { Injectable } from '@angular/core';
import { SubCategoryApiService } from '../api/sub-category-api.service';
import { SubСategory } from 'src/app/models/SubСategory';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SubCategoryBusinessLogicService {

  constructor(    private subCategoryApiService: SubCategoryApiService,
    ) { }

  public addSubcategory(subcategory: SubСategory): Observable<any> {
    return this.subCategoryApiService.addSubcategory(subcategory);
  }

  public deleteSubcategory(subcategoryId: number): Observable<any> {
    return this.subCategoryApiService.deleteSubcategory(subcategoryId);
  }

  public updateSubcategory(subcategory: SubСategory): Observable<any> {
    return this.subCategoryApiService.updateSubcategory(subcategory);
  }

}
