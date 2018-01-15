import { Pipe, PipeTransform } from '@angular/core';
import * as _ from 'lodash';
import { Chat } from '../_models/chat';
import { Message } from '_debugger';
import * as moment from 'moment';
@Pipe({
  name: 'orderBy'
})
export class OrderByPipe implements PipeTransform {
  transform(items: Chat[], field: string, order: 'asc' | 'desc'): Chat[] {
    if (!items) return [];
    //return _.orderBy(items, (item) => {return this.lastDate(item)}, order);
  }

  
}
