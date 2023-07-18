import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BudgetStatisticsDashBoardData } from 'src/app/models/BudgetStatisticsDashBoardData';

@Injectable({
  providedIn: 'root'
})
export class StatisticsApiService {

  private apiUrl2 : string = "https://localhost:7266/api/Statistics";


  constructor(private http: HttpClient) {}

  public getStatisticsData(): Observable<BudgetStatisticsDashBoardData> {
    return this.http.get<BudgetStatisticsDashBoardData>(`${this.apiUrl2}/get-statistics-data`);
  }


}
