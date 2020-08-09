"use strict";

const backup = require('./backup.js');

badBits = badBits || {};

badBits.setRender2d(function (dt, g) {

    //g.clear();

    let x = 0;
    let y = 0;
    let dtAccum = dt;

    for (x = 0; x < 320; x++) {

        for (y = 0; y < 240; y++) {
            g.setPixel(x, y, 0xFF, 0xFF, 0xFF,255);
        }
    
    }

    g.render();

});