import { Injectable } from '@angular/core';
import { ReportApiService } from '../../services/api/report-api.service';

@Injectable({
  providedIn: 'root',
})
export class ReportBusinessLogicService {
  constructor(
    private _reportApiService: ReportApiService,
  ) {}

  public getReport(startDate: string, endDate: string) {
    return this._reportApiService.getPeriodReport(startDate, endDate);
  }
}
