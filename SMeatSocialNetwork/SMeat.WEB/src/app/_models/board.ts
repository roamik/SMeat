

export class Board {

    id: string;
    name: string;
    text: string;
    likes: number;
    dislikes: number;

    constructor(id?: string, name?: string, text?: string, likes?: number, dislikes?: number) {
        this.id = id;
        this.name = name;
        this.text = text;
        this.likes = likes;
        this.dislikes = dislikes;
    }

}



