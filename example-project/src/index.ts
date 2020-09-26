import { IBadBits, IGamepadState, I2dContext } from '../../Typescript/index';
import { examples } from './examples';

// this must be done so 'engine' gets recognised as an external global.
declare var engine: IBadBits;

// counter to keep track of what example we are on
let exampleIndex: number = 0;

// previous input state. Used to compare old values against new ones.
let oldState: IGamepadState = {} as IGamepadState;

// initialize all the demos in the demo application.
engine.setInit(() => {

    examples.forEach(e => e.init(engine));

});

// tell the engine to use whatever the current example is for drawing the background.
engine.setDrawBackground((dt, context) => {

    examples[exampleIndex].drawBackground(dt, context);

});

// tell the engine to draw 3d for whatever the current example is
engine.setDraw3d((dt,context)=>{
    examples[exampleIndex].draw3d(dt, context);

});

// handle the gamepad input.
engine.setProcess((_, ctx) => {
    const state = ctx.pollGamepadState();

    // handle the key presses.
    if (state.up && !oldState.up) {
        exampleIndex--;
    }
    else if (state.down && !oldState.down) {
        exampleIndex++;
    }

    oldState = state;

    // make sure the counter is in the correct position.
    if (exampleIndex === examples.length) { exampleIndex = 0; } 
    else if (exampleIndex < 0) { exampleIndex = examples.length - 1; }
});

// a silly little function to draw light text against a dark background.
const drawText = (ctx:I2dContext, x:number,y:number, text:string)=>{
    ctx.drawDarkText(x+-1,y+-1,text);
    ctx.drawDarkText(x+0,y+-1,text);
    ctx.drawDarkText(x+-1,y+0,text);
    ctx.drawLightText(x+0,y+0,text);
}

// draw everything in the foreground that the example wants to, then put some text on top of that.
engine.setDrawForeground((dt, context) => {

    examples[exampleIndex].drawForeground(dt, context);

   drawText(context,5, 5, "Bad-Bits Engine \n"
    + "Press Up/Dn to change example.\n"
    + examples[exampleIndex].name);

});
