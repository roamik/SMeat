

export class Board {

    id: string;
    name: string;
    text: string;
    likes: string[];
    dislikes: string[];

    constructor(id?: string, name?: string, text?: string) {
        this.id = id;
        this.name = name;
        this.text = text;
    }

}



