import { Injectable } from '@angular/core';

@Injectable()
export class EnumToArrayHelper {

  constructor() { }

  public enumSelector(definition, iconDefinition?) {

    var selectors = Object.keys(definition);
    var iconSelectors = iconDefinition? Object.keys(iconDefinition): null;

    return selectors.slice(selectors.length / 2).map(key => ({ value: definition[key], title: key, icon: iconSelectors ? iconDefinition[definition[key]]:'' }));
  }
}

