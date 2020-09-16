import { I2dContext, I3dContext, IBadBits } from '../../../Typescript/index';

import { IExample } from './index';

let time = 0.0;
let b: IBadBits;

const example: IExample = {
    name: 'simple-sprite',
    draw3d: () => { },
    drawBackground: () => { },
    drawForeground: (dt, ctx) => {

        for (let y = 0; y < 240; y += 16) {
            for (let x = 0; x < 320; x += 16) {
                ctx.drawSprite('coins', { x: x, y: y, width: 16, height: 16 }, false, time+(x*y)*0.0010);

            }
        }

        time = time + (dt);

        b.log(time.toString());



    },

    init: (bb) => {
        bb.loadTexture('coins', 'assets/coins.png');
        bb.loadSprite('coins', 'assets/coins.json');
        b = bb;
    }
};

export default example;
