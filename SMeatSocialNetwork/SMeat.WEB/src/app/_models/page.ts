export class Page {
    page: number;
    count: number;
    searchBy: string;
    from:string;
    constructor(page: number = 0, count: number = 5, from:string = '', searchBy:string = ''){
      this.page = page;
      this.count = count;
      this.searchBy = searchBy;
      this.from = from;
    }
  }