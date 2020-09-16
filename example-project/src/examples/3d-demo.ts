import { I2dContext, I3dContext, IBadBits } from '../../../Typescript/index';

import { IExample } from './index';

import {Matrix4} from './util/matrix-math';

let time = 0.0;
let b: IBadBits;

let r = 0;

const example: IExample = {
    name: '3d example',
    draw3d: (dt, ctx) => {

        ctx.setView(0,0,-1,0,0,0);

        let m4:Matrix4 = new Matrix4();

        

        r=r+dt;

        m4.rotateY(r);
        m4.scale(4,4,4);

        ctx.drawColoredTriangles({ r: 0, g: 0, b: 0, a: 255 }, m4.transform([
            { x: -1, y: -1, z: -1 },
            { x: 1, y: -1, z: -1 },
            { x: 1, y: 1, z: -1 },

            { x: -1, y: -1, z: -1 },
            { x: -1, y: 1, z: -1 },
            { x: 1, y: 1, z: -1 },

        ]));


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
