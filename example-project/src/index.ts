import { IBadBits, IGamepadState, I2dContext } from '../../Typescript/index';

import { examples } from './examples';
declare var engine: IBadBits;
declare var __dirname: string;

let exampleIndex: number = 0;
let oldState: IGamepadState = {} as IGamepadState;

engine.setInit(() => {

    examples.forEach(e => e.init(engine));

});

engine.setDrawBackground((dt, context) => {

    examples[exampleIndex].drawBackground(dt, context);

});

engine.setProcess((dt, ctx) => {
    const state = ctx.pollGamepadState();

    if (state.up && !oldState.up) {
        exampleIndex--;
    }
    else if (state.down && !oldState.down) {
        exampleIndex++;
    }

    oldState = state;

    if (exampleIndex === examples.length) { exampleIndex = 0; } 
    else if (exampleIndex < 0) { exampleIndex = examples.length - 1; }
});

const drawText = (ctx:I2dContext, x:number,y:number, text:string)=>{
    ctx.drawDarkText(-1,-1,text);
    ctx.drawDarkText(0,-1,text);
    ctx.drawDarkText(-1,0,text);
    ctx.drawLightText(0,0,text);
}

engine.setDrawForeground((dt, context) => {

    examples[exampleIndex].drawForeground(dt, context);

   drawText(context,-1, -1, "Bad-Bits Engine \n"
    + "Press Up/Dn to change example.\n"
    + examples[exampleIndex].name);

});
