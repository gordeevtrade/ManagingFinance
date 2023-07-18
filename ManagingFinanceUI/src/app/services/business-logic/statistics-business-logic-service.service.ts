import { Injectable } from '@angular/core';
import { StatisticsApiService } from '../api/statistics-api.service';
import { BudgetStatisticsDashBoardData } from 'src/app/models/BudgetStatisticsDashBoardData';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StatisticsBusinessLogicServiceService {

  constructor(private _statisticsApiService: StatisticsApiService, ) { 

  }

  public getStatistics(): Observable<BudgetStatisticsDashBoardData> {
    return this._statisticsApiService.getStatisticsData();
  }

}
