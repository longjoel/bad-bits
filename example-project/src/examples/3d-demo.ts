import { I2dContext, I3dContext, IBadBits, IVertexPosition, IVertexTexture } from '../../../Typescript/index';

import { IExample } from './index';

let time = 0.0;
let b: IBadBits;

let r = 0;

const v = {
    a: [-1, 1, -1], b: [-1, 1, 1], c: [1, 1, 1], d: [1, 1, -1],
    e: [-1, -1, -1], f: [-1, -1, 1], g: [1, -1, 1], h: [1, -1, -1]
};

const edges = [
    v.a, v.b, v.c, v.a, v.c, v.d,
    v.a, v.d, v.e, v.e, v.h, v.d,
    v.d, v.h, v.g, v.d, v.g, v.c,
    v.b, v.c, v.f, v.f, v.c, v.g,
    v.a, v.b, v.f, v.a, v.f, v.e,
    v.e, v.f, v.g, v.e, v.g, v.h
];

const vertices = edges.map(e => { return { x: e[0], y: e[1], z: e[2] } as IVertexPosition });

const example: IExample = {
    name: '3d example',
    draw3d: (dt, ctx) => {

        ctx.setView(0, 2, -5, 0, 0, 4);
        r = r + (dt * 20);
        const transform = {
            scaleX: 1, scaleY: 1, scaleZ: 1, 
            roll: 0, pitch: r * 0.0174533 * 1.5, yaw:  r * 0.0174533, 
            translateX: 0, translateY: 0, translateZ: 4
};
        ctx.drawTexturedTriangles('grass', [
            { x: -5, y: 0, z: -5, u: 0, v: 0 },
            { x: -5, y: 0, z: 5, u: 10, v: 0 },
            { x: 5, y: 0, z: 5, u: 10, v: 10 },

            { x: -5, y: 0, z: -5, u: 0, v: 0 },
            { x: 5, y: 0, z: -5, u: 0, v: 10 },
            { x: 5, y: 0, z: 5, u: 10, v: 10 }
        ] as IVertexTexture[], transform);

        ctx.drawColoredTriangles({ r: 0, g: 0, b: 0, a: 255 }, vertices, transform);

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
        b.loadTexture('grass', 'assets/grass.png');
    }
};

export default example;
