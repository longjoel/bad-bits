'use strict';

engine.setInit(function () {

    engine.createTexture('alpha', 64, 64);
    engine.loadTexture('beta', __dirname + '/up.png');

    let x, y;

    for (y = 0; y < 64; y++) {
        for (x = 0; x < 64; x++) {
            if(x %2 == 0 && y%2 == 0 && (x+y)%3 == 0){
            engine.setPixel('alpha',
                x, y,
               0,0,255);
            }else{
                engine.setPixelTransparent('alpha',x,y,0,0,0,0);
            }
        }
    }

});

let t = 0;

engine.setRender2d(function (dt) {
    t = t + (dt * 2);
    engine.drawTexture('beta', [0, 0, 64, 64], [0, 0, 320, 240]);
    engine.drawTexture('alpha', [0, 0, 64, 64], [(t * 5) % 200, 0, 128, 128]);
});