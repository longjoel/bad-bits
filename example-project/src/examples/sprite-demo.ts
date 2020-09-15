import { I2dContext, I3dContext, IBadBits } from '../../../Typescript/index';

import {IExample} from './index';

let time = 0.0;
let b:IBadBits;

const example:IExample = {
    name:'simple-sprite',
    draw3d:()=>{},
    drawBackground:()=>{},
    drawForeground:(dt, ctx)=>{

        ctx.drawSprite('coins',128,128,time);

        time=time+(dt*10);

        b.log(time.toString());
        


    },

    init:(bb)=>{
        bb.loadTexture('coins','assets/coins.png');
        bb.loadSprite('coins','assets/coins.json');
        b = bb;
    }
};

export default example;
