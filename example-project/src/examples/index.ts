import { I2dContext, I3dContext, IBadBits } from '../../../Typescript/index';

import spriteDemo from './sprite-demo';
import textureDemo from './texture-demo';

export interface IExample {

    name: string;
    init: (badBits: IBadBits) => void;
    drawBackground: (dt: number, ctx: I2dContext) => void;
    drawForeground: (dt: number, ctx: I2dContext) => void;
    draw3d: (dt: number, ctx: I3dContext) => void;
};


export const examples: IExample[] = [
    {
        name: '_',
        init: () => { },
        draw3d: () => { },
        drawBackground: () => { },
        drawForeground: () => { },
    },
    spriteDemo,
    textureDemo
];