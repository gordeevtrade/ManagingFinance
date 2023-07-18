import { SubСategory } from "./SubСategory";

export class Category {
    id: number;
    name: string;
    description: string;
    subcategories?: SubСategory[];

    constructor(id: number, name: string, description: string) {
      this.id = id;
      this.name = name;
      this.description = description;
    }
  }
