import { I2dContext, I3dContext, IAudioContext, IBadBits } from '../../../Typescript/index';

import spriteDemo from './sprite-demo';
import textureDemo from './texture-demo';
import threedee from './3d-demo';
import sound from './audio';

export interface IExample {

    name: string;
    init: (badBits: IBadBits) => void;
    drawBackground: (dt: number, ctx: I2dContext) => void;
    drawForeground: (dt: number, ctx: I2dContext) => void;
    draw3d: (dt: number, ctx: I3dContext) => void;
    process?:(badBits:IBadBits, ctx:IAudioContext)=>void;
};


export const examples: IExample[] = [
    {
        name: '',
        init: () => { },
        draw3d: () => { },
        drawBackground: () => { },
        drawForeground: () => { }       
    },
    spriteDemo,
    textureDemo,
    threedee,
    sound
];