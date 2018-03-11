export class Reply {

  id: string;
  text: string;
  boardId: string;

  replyId: string[];

  constructor(id?: string, text?: string, boardId?: string)
  {
    this.id = id;
    this.text = text;
    this.boardId = boardId;

    this.replyId = [];
  }

  getId()
  {
    return this.id;
  }
}
