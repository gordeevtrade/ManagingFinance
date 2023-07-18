import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({providedIn: 'root'})
export class ResultService {
   private _result = new BehaviorSubject<any>(null);
  public currentResult = this._result.asObservable();
    public changeResult(value: any) {
    this._result.next(value);
  }
}
