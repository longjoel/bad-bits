import { I2dContext, I3dContext, IBadBits } from '../../../Typescript/index';

import { IExample } from './index';


let xAccumulator = 1.0;
let yAccumulator = 1.0;
let xDirection = 1;
let yDirection = 1;

const example:IExample= {
    name: 'draw simple texture',
    init: (bb) => {

        bb.createTexture('blue', 2, 2);
        bb.setPixel('blue', 0, 0, { r: 0, g: 0, b: 255, a: 255 });
        bb.setPixel('blue', 1, 1, { r: 0, g: 0, b: 128, a: 255 });

    },
    draw3d: () => { },
    drawBackground: (dt, ctx) => {

        xAccumulator += (dt * xDirection) * 20;
        yAccumulator += (dt * yDirection) * 30;
        if (yAccumulator < 0) { yAccumulator = 0; yDirection = -yDirection; }
        if (yAccumulator > 240) { yAccumulator = 239; yDirection = -yDirection; }
        if (xAccumulator < 0) { xAccumulator = 0; xDirection = -xDirection; }
        if (xAccumulator > 240) { xAccumulator = 239; xDirection = -xDirection; }
        for (let y = 0; y < 120; y++) {
            ctx.drawTexture('blue', { x: 0, y: 0, width: 2, height: 2 }, { x: xAccumulator, y: y * 2, width: 2, height: 2 });
            ctx.drawTexture('blue', { x: 0, y: 0, width: 2, height: 2 }, { x: y * 2, y: yAccumulator, width: 2, height: 2 });

        }

    },
    drawForeground: () => { },
};

export default example;