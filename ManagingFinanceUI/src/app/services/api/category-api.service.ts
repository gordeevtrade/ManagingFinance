import { Injectable } from '@angular/core';
import {environment} from "../../../environments/environment";
import {Observable} from "rxjs/internal/Observable";
import {Category} from "../../models/Category";
import {HttpClient} from "@angular/common/http";
import { CategoryCollection } from 'src/app/models/CategoryCollection';

@Injectable({
  providedIn: 'root'
})
export class CategoryApiService {
  private categoryUrl = `${environment.apiUrl}/CategoryType`;

  constructor(private http:HttpClient) {
  }

  public getCategory(): Observable<Category[]> {
    return this.http.get<Category[]>(this.categoryUrl);
  }

  public getCategoryCollection(): Observable<CategoryCollection> {
    const url = `${this.categoryUrl}/category-data-collection`;
    return this.http.get<CategoryCollection>(url);
  }

}
