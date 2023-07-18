import { Injectable } from '@angular/core';
import { SubСategory } from '../../models/SubСategory';

@Injectable({
  providedIn: 'root'
})
export class CategoryDataTransferService {
  private subcategory: SubСategory | undefined;
  constructor() { }

  public setSubcategory(subcategor: SubСategory): void {
    this.subcategory = { ...subcategor };

  }
  public getSubcategory(): SubСategory | undefined {
    return this.subcategory;
  }
}
