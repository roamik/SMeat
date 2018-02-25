

export class Board {

    id: string;
    name: string;
    text: string;
    likes: number;
    dislikes: number;

    constructor(id: string, name: string, text: string, likes?: number, dislikes?: number) {
        this.id = id;
        this.name = name;
        this.text = text;
        this.likes = likes || 0;
        this.dislikes = dislikes || 0;
    }

    voteUp(): boolean {
        this.likes++;
        return false;
    }

    voteDown(): boolean {
        this.dislikes++;
        return false;
    }

}



