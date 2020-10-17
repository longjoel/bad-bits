// Import the root engine object so we can keep a handle on it while not initializaing.
import { IBadBits } from '../../../Typescript/index';

// Grab the example interface from the root.
import { IExample } from './index';

// accumulated time in seconds
let time = 0.0;

// handle to the engine
let b: IBadBits;

const example: IExample = {

    // each example needs a name
    name: 'simple-sprite',

    // no 3d in this example: NOOP
    draw3d: () => { },

    // NOOP
    drawBackground: () => { },

    // draw against the forground.
    drawForeground: (dt, ctx) => {

        // sprite is 16x16 pixels.
        for (let y = 0; y < 240; y += 16) {
            for (let x = 0; x < 320; x += 16) {

                // draw an even number of coin sprites but come up with something to make them not all look the same.
                // we are not ignoring the aspect ratio so the final image will be centered inside of the destination
                // rectangle.
                ctx.drawSprite('coins', {
                    x: x,
                    y: y,
                    width: 16,
                    height: 16
                },
                    false,
                    time + (x * y) * 0.0010);
            }
        }

        time = time + (dt); // keep track of how much time has passed since the demo started.
    },

    // initialize the demo
    init: (bb) => {
        // load the coin texture
        bb.loadTexture('coins', 'assets/coins.png');
        // load the metadata that turns a collection of source rectangles into a sprite.
        bb.loadSprite('coins', 'assets/coins.json');
        b = bb;
    }
};

export default example;
