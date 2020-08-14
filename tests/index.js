"use strict";

const backup = require('./backup.js');

badBits.setRender2d(function (dt, g) {

    g.clear();

    g.setRect(0, 0, 16, 16, 255, 255, 255, 0xFF);
    g.setRect(0, 240 - 16, 16, 16, 128, 128, 128, 0xFF);
    g.setRect(320 - 16, 240 - 16, 16, 16, 128, 128, 0, 0xFF);

    let x;
    let y;
    
    for (y = 50; y < 190; y++) {
        for (x = 50; x < 270; x++) {
            g.setPixel(x,y,(x*y)%255,(x+y)%255,128,255);
        }
    }
    
    g.render();

});