import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PeriodReport } from '../../models/PeriodReport';
import { Observable } from 'rxjs';
import { BudgetStatisticsDashBoardData } from 'src/app/models/BudgetStatisticsDashBoardData';

@Injectable({
  providedIn: 'root'
})
export class ReportApiService {
  private apiUrl : string = "https://localhost:7266/api/Report";

  constructor(private http: HttpClient) {}

  public getDailyReport(date: string): void {
    const options = { params: { date: date } };

    this.http.get(this.apiUrl, options)
      .subscribe((data: any) => {
        console.log(data);
      }, (error) => {
        console.error(error);
      });
  }

  getPeriodReport(startDate: string, endDate: string): Observable<PeriodReport> {
    const url = `${this.apiUrl}/get-period-report?startDate=${startDate}&endDate=${endDate}`;
    return this.http.get<PeriodReport>(url);
  }



  }
