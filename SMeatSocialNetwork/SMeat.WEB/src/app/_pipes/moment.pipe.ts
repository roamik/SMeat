import { Pipe, PipeTransform } from '@angular/core';
import * as moment from 'moment';

@Pipe({
  name: 'momentFormat'
})
export class MomentFormatPipe implements PipeTransform {
  transform(date, format = 'MMMM Do YYYY, h:mm:ss a'): any {
    if(!moment(date).isValid){
      return '';
    }
    return moment(date).format(format);
  }
}

@Pipe({
  name: 'momentCalendar'
})
export class MomentCalendarPipe implements PipeTransform {
  transform(date): any {
    if(!moment(date).isValid){
      return '';
    }
    return moment(date).calendar();
  }
}