import { Injectable } from '@angular/core';

@Injectable()
export class EnumToArrayHelper {

  constructor() { }

  public enumSelector(definition) {

    var selectors = Object.keys(definition);

    return selectors.slice(selectors.length / 2).map(key => ({ value: definition[key], title: key }));
  }
}

