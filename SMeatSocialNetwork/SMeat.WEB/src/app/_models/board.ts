export class Board {

    id: string;
    name: string;
    text: string;
    likes;
    dislikes;

    constructor(id?: string, name?: string, text?: string, likes?, dislikes?) {
        this.id = id;
        this.name = name;
        this.text = text;
        this.likes = likes;
        this.dislikes = dislikes;
    }

}



