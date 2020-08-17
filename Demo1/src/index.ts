import { IBadBits } from './bad-bits';

declare var engine: IBadBits;
declare var __dirname: string;

engine.setInit(() => {

    engine.loadSpriteSheet('font', 'asset\\font.png', 15, 15);

});

const drawChar = (x: number, y: number, c: string) => {

    const charTable = [' !"#$&\'()*+,-./',
        '0123456789:;<=>?',
        '@ABCDEFGHIJKLMNO',
        'PQRSTUVWXYZ[\\]^_',
        '`abcdefghijklmno',
        'pqrstuvwxyz{|}~'];

    const searchChar = (cx: string) => {

        for (let row = 0; row < charTable.length; row++) {
            for (let col = 0; col < charTable[row].length; col++) {
                if (charTable[row][col] == cx[0]) {
                    return [row, col];
                }
            }
        }

        return [-1, -1];
    }

    const [row,col] = searchChar(c);
    
    if(row >= 0 && col >= 0){
        engine.drawSprite('font',x,y,row,col);
    }

}

engine.setRender2d((dt: number) => {

    drawChar(16,16,'a');
    drawChar(32,16,'b');


});