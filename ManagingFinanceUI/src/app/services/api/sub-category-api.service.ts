import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { SubСategory } from '../../models/SubСategory';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})

export class SubCategoryApiService {
  private subCategoryUrl = `${environment.apiUrl}/Category`;

  constructor(private http:HttpClient) {
  }


  public getSubCategoriesByCategoryID(categoryTypeId: number): Observable<SubСategory[]> {
    const url = `${this.subCategoryUrl}/categorytype/${categoryTypeId}`;
    return this.http.get<SubСategory[]>(url);
  }

  // public getAllSubCategories(): Observable<SubСategory[]> {
  //   return this.http.get<SubСategory[]>(this.subCategoryUrl);
  // }

  public  addSubcategory(subcategory: SubСategory): Observable<SubСategory> {
    return this.http.post<SubСategory>(this.subCategoryUrl, subcategory);
  }

  public  deleteSubcategory(id:number): Observable<void> {
    const url = `${this.subCategoryUrl}/${id}`;
    return this.http.delete<void>(url);
  }

  public  updateSubcategory(subcategory: SubСategory): Observable<void> {
    const url = `${this.subCategoryUrl}`;
    return this.http.put<void>(url, subcategory);
  }
}
