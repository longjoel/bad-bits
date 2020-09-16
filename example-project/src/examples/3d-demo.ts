import { I2dContext, I3dContext, IBadBits } from '../../../Typescript/index';

import { IExample } from './index';

let time = 0.0;
let b: IBadBits;

const example: IExample = {
    name: '3d example',
    draw3d: (dt, ctx) => {

        ctx.setView(-10,5,-10,0,0,0)

        ctx.drawColoredTriangles({ r: 0, g: 0, b: 0, a: 255 }, [
            { x: -20, y: 0, z: 0 },
            { x: 20, y: 0, z: 0 },
            { x: 0, y: 30, z: 0 }
        ]);

    },

    drawBackground: (dt, ctx) => {

        ctx.drawTexture('clouds',
            {
                x: 0,
                y: 0,
                width: 320,
                height: 240
            }, {
                x: 0,
            y: 0,
            width: 320,
            height: 240
        });
    },

    drawForeground: (dt, ctx) => {

    },

    init: (bb) => {
        b = bb;

        b.loadTexture('clouds', 'assets/clouds.png');
    }
};

export default example;
