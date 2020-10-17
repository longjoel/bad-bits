import { I2dContext, I3dContext, IBadBits, IVertexPosition, IVertexTexture } from '../../../Typescript/index';

import { IExample } from './index';

let badBits:IBadBits;

const example:IExample = {

    draw3d:()=>{},
    drawBackground:()=>{},
    drawForeground:()=>{},
    init:(bb)=>{badBits=bb;
    
        badBits.loadAudio('city','assets/city.wav');
    },
    name:'sound example'

};

export default example;