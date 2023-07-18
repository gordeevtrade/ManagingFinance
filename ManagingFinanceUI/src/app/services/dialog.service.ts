import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

@Injectable({
  providedIn: 'root'
})
export class DialogService {
  constructor(private dialog: MatDialog) {}


  public openDialog(component: any, config: { width: string, height: string, [key: string]: any }) {
    const enterAnimationDuration = '0ms';
    const exitAnimationDuration = '0ms';
    const { width, height, ...otherConfig } = config;
    return this.dialog.open(component, {
      panelClass: 'modal-dialog',
      width,
      height,
      enterAnimationDuration,
      exitAnimationDuration,
      ...otherConfig,
    });
  }







}
