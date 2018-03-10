export class Reply {

  id: string;
  text: string;
  boardId: string;

  constructor(id?: string, text?: string, boardId?: string)
  {
    this.id = id;
    this.text = text;
    this.boardId = boardId;
  }
}
