import { I2dContext, I3dContext, IBadBits } from '../../../Typescript/index';

import { IExample } from './index';

let time = 0.0;
let b: IBadBits;

const example: IExample = {
    name: '3d example',
    draw3d: (dt,ctx) => { 

        ctx.drawColoredTriangles({r:128,g:128,b:128,a:128},[
            {x:0,y:0,z:0},
            {x:10,y:0,z:0},
            {x:5,y:10,z:0}
        ]);

    },

    drawBackground: (dt,ctx) => { 

        ctx.drawTexture('clouds',{x:0,y:0,width:320,height:240},{x:0,y:0,width:320,height:240});


    },

    drawForeground: (dt, ctx) => {

    },

    init: (bb) => {
        b = bb;

        b.loadTexture('clouds','assets/clouds.png');
    }
};

export default example;
